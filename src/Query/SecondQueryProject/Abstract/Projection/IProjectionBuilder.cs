namespace SecondQueryProject.Abstract.Projection;

public interface IProjectionBuilder<T> where T : ReadModel.ReadModel
{
    public T ProjectModel(T readModel);
}