using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class NonRecreatableBuilder : IModelBuilder<Car>
    {
        public Car Build()
        {
            return new Car { Name = "Not a model car" };
        }
    }
}
