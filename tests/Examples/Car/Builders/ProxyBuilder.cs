using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class ProxyBuilder : ModelBuilder<Garage>
    {
        private CarBuilder _car;

        public ProxyBuilder(IBuilderFactory factory) : base(factory)
        {
            _car = _factory.Create<CarBuilder>();
        }

        public ProxyBuilder AddWheel(WheelBuilder wheel) => Set(() => _car.AddWheel(wheel));
        public ProxyBuilder AddWheel(Action<WheelBuilder> action)
        {
            return Set(() => _car.AddWheel(FromAction<WheelBuilder, Wheel>(action)));
        }
        public ProxyBuilder WithName(string name) => Set(() => _car.WithName(name));

        protected override Garage BuildModel()
        {
            return new Garage()
            {
                Car = _car,
                Address = "127.0.0.1"
            };
        }

        public static implicit operator Garage(ProxyBuilder builder) => builder.Build();

        protected override ProxyBuilder Set(Action action) => Set<ProxyBuilder>(action);
        public override ProxyBuilder Recreate() => Recreate<ProxyBuilder>();
    }
}
