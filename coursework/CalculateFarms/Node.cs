namespace CalculateFarms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        private double _x;
        private double _y;

        private List<Force> _forces = new List<Force>();
        private List<Force> _reactions = new List<Force>();

        public Node(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Node()
        {
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public List<Force> Forces
        {
            get { return _forces; }
        }

        public List<Force> Reactions
        {
            get { return _reactions; }
        }

        public double GetDist(Node node)
        {
            return Math.Sqrt(((_x - node.X) * (_x - node.X)) + ((_y - node.Y) * (_y - node.Y)));
        }

        public double SumForcesX()
        {
            return _forces.Select(forse => forse.GetProjectionX()).Sum();
        }

        public double SumForcesY()
        {
            return _forces.Select(forse => forse.GetProjectionY()).Sum();
        }

        public double SumReactionsX()
        {
            return _reactions.Select(reaction => reaction.GetProjectionX()).Sum();
        }

        public double SumReactionsY()
        {
            return _reactions.Select(reaction => reaction.GetProjectionY()).Sum();
        }

        public double GetAnglePoint(Node node)
        {
            double cathetUp = Math.Abs(_y - node.Y);
            double cathetDown = Math.Abs(_x - node.X);

            if (cathetDown < 1e-9)
            {
                return Math.PI / 2;
            }

            return Math.Atan(cathetUp / cathetDown);
        }

        public double OrientForces(int numForce, Node node)
        {
            return OrientForMoment(_forces[numForce], node);
        }

        public double OrientReactions(int numReaction, Node node)
        {
            return OrientForMoment(_reactions[numReaction], node);
        }

        private double OrientForMoment(Force force, Node node)
        {
            double a = Math.Sin(force.Angle);
            double b = -Math.Cos(force.Angle);
            double c = -(_x * a) - (_y * b);

            double dist = Math.Abs((a * node.X) + (b * node.Y) + c) / Math.Sqrt((a * a) + (b * b));

            Node node1 = new Node();

            if (Math.Abs(Math.Cos(force.Angle)) > 1e-5)
            {
                node1._x = _x + Math.Cos(force.Angle);
            }
            else
            {
                node1._x = _x + Math.Sin(force.Angle);
            }

            node1._y = ((a * node1._x) + c) / -b;

            bool orient = GetSignOrient(node1, node);

            if (!orient)
            {
                return -dist;
            }

            return dist;
        }

        private bool GetSignOrient(Node cur, Node check)
        {
            Node v1 = new Node(_x - cur.X, _y - cur.Y);
            Node v2 = new Node(check.X - cur.X, check.Y - cur.Y);

            return ((v1._x * v2._y) - (v2._x * v1._y)) < 0;
        }
    }
}
