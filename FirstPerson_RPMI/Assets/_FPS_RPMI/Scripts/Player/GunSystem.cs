using UnityEngine;
using UnityEngine.InputSystem;

public class GunSystem : MonoBehaviour
{
    #region General Variables
    [Header("General References")]
    [SerializeField] Camera fpsCam; //ref si disparamos desde el centro de la cam.
    [SerializeField] Transform shootPoint; //Ref si disparamos desde la punta del cañon.
    [SerializeField] LayerMask impactLayer; //Layer con la que interactua el raycast.
    RaycastHit hit; //Almacen de la informacion de los objetos con los que el raycast puede chocar.

    [Header("Weapon Parameters")]
    [SerializeField] int damage = 10; //Daño del arma por bala.
    [SerializeField] float range = 100f; //Distancia maxima de disparo.
    [SerializeField] float spread = 0; //radio de dispersion.
    [SerializeField] float shootingCooldown = 0.2f; //Tiempo entre disparos.
    [SerializeField] float reloadTime = 1.5f; //Tiempo de recarga en segundos.
    [SerializeField] bool allowButtonHold = false; //Si el disparo se ejecuta por click(false) o por mantener (true).

    [Header("Bullet Management")]
    [SerializeField] int ammoSize = 30; //Cantidad maxima de municion por cargador.
    [SerializeField] int BulletsPerTap = 1; //Cantidad de balas disparadas por ejecucion del disparo.
    int bulletsLeft; //Cantidad de municion restante

    [Header("FeedbackReferences")]
    [SerializeField] GameObject impactEffect; //Ref al VFX de impacto de bala.

    [Header("Dev - Gun State Bools")]
    [SerializeField] bool shooting; //Indica si se esta disparando.
    [SerializeField] bool canShoot; //Indica si se puede disparar.
    [SerializeField] bool reloading; //Indica si estamos en proceso de recarga.
    #endregion

    private void Awake()
    {
        bulletsLeft = ammoSize;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        //Este es el metodo mas importante
        //Se defube disparo por Raycast -> Utilizable por cualquier mecanica

        //Almacenar la direccion del disparo y modicicarla en caso de haber dispersion
        Vector3 direction = fpsCam.transform.forward;

        //Añadir dispersion aleatoria segun el valor de spread
        direction.x += Random.Range(spread, spread);
        direction.y += Random.Range(spread, spread);

        //Declaracion del raycast
        //Physics.Raycast(Origen del rayo, direccion, almacen de la info del impacto, longitud del rayo, capa de)
        if(Physics.Raycast(fpsCam.transform.position, direction, out hit, range, impactLayer))
        {
            //Aqui podemos codear todos los efectos que quiero para la interaccion;
            Debug.Log(hit.collider.name);
        }
    }

    #region Inputs

    public void OnShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    public void Onreload(InputAction.CallbackContext context)
    {

    }

    #endregion
}
