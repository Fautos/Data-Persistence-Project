using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent_manager : MonoBehaviour
{
    public static Persistent_manager Instance;
    public string userName;

    // Método para relizar la continuidad entre escenas
    private void Awake()
    {
        // Patrón "Singleton"
        // Con esto evitamos que se generen infinitos MainManager volver al menú de título
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    
}
