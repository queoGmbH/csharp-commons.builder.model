using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class NonModelBuilder : IModelBuilder<Car>, IRecreatable<NonModelBuilder>
    {
        public Car Build()
        {
            return new Car { Name = "Not a model car" };
        }

        public NonModelBuilder Recreate()
        {
            return new NonModelBuilder();
        }
    }
}
