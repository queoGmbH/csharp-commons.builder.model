using System.Collections.Generic;

namespace Queo.Commons.Builders.Model.Examples.Car
{
    public class Car
    {
        public string Name { get; set; }
        public IList<Wheel> Wheels { get; set; } = new List<Wheel>();


        public override string ToString() => $"Car: {Name}";

    }
}
