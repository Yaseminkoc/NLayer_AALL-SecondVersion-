namespace NLayer.Core.Results.Concrete
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data):base(data,true)
        {

        }
    }
}
