using UnityEngine;
using Microsoft.Extensions.DependencyInjection;

public class StartGameController : MonoBehaviour
{
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
