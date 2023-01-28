using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class InvalidBuilder : ModelBuilder<object>
    {
        private Car _car = new Car();
        private Garage _garage = new Garage();
        private string _name = "...";

        public InvalidBuilder(IBuilderFactory factory) : base(factory)
        {
        }

        public InvalidBuilder WithName(string name) => Set(() => _name = name);
        public InvalidBuilder WithCar(Car car) => Set(() => _car = car);
        public InvalidBuilder WithName(Garage garage) => Set(() => _garage = garage);

        protected override object BuildModel()
        {
            _garage.Address = _name;
            _garage.Car = _car;
            return _garage;
        }

        protected override InvalidBuilder Set(Action action) => Set<InvalidBuilder>(action);
    }
}
