namespace DDNSUpdater.Interfaces;

public interface IDDNSService
{
    public void Init();
    public void Update(bool changed);
    public void SetUpdateURL();
}