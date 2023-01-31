namespace DDNSUpdater.Interfaces;

public interface IDDNSService
{
    public void Start();
    public void Update();
    public void SetUpdateURL();
}