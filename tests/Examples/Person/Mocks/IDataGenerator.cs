namespace Queo.Commons.Builders.Model.Examples.Person.Mocks
{
    public interface IDataGenerator
    {
        int GetInt(int min = int.MinValue, int max = int.MaxValue);
        string GetString(int min = 4, int max = 10);
    }
}
