using Cysharp.Threading.Tasks;

public interface ILoader
{
    public UniTask Load(string path);
}
