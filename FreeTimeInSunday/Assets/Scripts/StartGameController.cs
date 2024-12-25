using UnityEngine;
using Microsoft.Extensions.DependencyInjection;

public class StartGameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var services = new ServiceCollection();
        services.AddSingleton<PlayerPresenter>();
        services.BuildServiceProvider().GetService<PlayerPresenter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
