using System;
using System.Collections.Generic;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class CarBuilder : ModelBuilder<Car>
    {
        private string _name;
        private BuilderCollection<WheelBuilder, Wheel> _wheels;

        public CarBuilder(IBuilderFactory factory) : base(factory)
        {
            _name = $"CarName {BuilderIndex}";
            _wheels = new BuilderCollection<WheelBuilder, Wheel>(_factory);
        }

        public CarBuilder WithName(string name) => Set(() => _name = name);
        public CarBuilder AddWheel(WheelBuilder wheel) => Set(() => _wheels.Add(wheel));
        public CarBuilder AddWheel(Action<WheelBuilder> action) => Set(() => _wheels.Add(action));
        public IEnumerable<WheelBuilder> GetWheels() => _wheels.GetBuilders<WheelBuilder>();

        protected override Car BuildModel()
        {
            Car car = new Car();
            car.Name = _name;
            foreach (var wheel in _wheels)
            {
                car.Wheels.Add(wheel.Build());
            }
            return car;
        }

        public override CarBuilder Recreate() => Recreate<CarBuilder>();
        protected override CarBuilder Set(Action action) => Set<CarBuilder>(action);
    }
}
