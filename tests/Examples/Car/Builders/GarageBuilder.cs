using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class GarageBuilder : ModelBuilder<Garage>
    {

        private string _address;
        private CarBuilder _car;

        public GarageBuilder(IBuilderFactory factory) : base(factory)
        {
            _address = $"Street No. {BuilderIndex}";
            _car = _factory.Create<CarBuilder>();
        }

        public GarageBuilder WithAddress(string address) => Set(() => _address = address);
        public GarageBuilder WithCar(CarBuilder car) => Set(() => _car = car);
        public GarageBuilder WithCar(Action<CarBuilder> builderAction) => Set(() =>
        {
            _car = FromAction<CarBuilder, Car>(builderAction);
        });

        protected override Garage BuildModel()
        {
            return new Garage
            {
                Address = _address,
                Car = _car.Build()
            };
        }

        public override GarageBuilder Recreate() => Recreate<GarageBuilder>();
        protected override GarageBuilder Set(Action action) => Set<GarageBuilder>(action);
        public static implicit operator Garage(GarageBuilder builder) => builder.Build();
    }
}
