using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyOnMultipleHit : MonoBehaviour
{
    [SerializeField] private int maxHitCount = 10;
    [SerializeField] private bool randomHitCount = true;

    private Material _material;
    private float _destroyStepsPercent = 1;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    public GameManager GameManager { set => _gameManager = value; }

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _audioSource = GetComponent<AudioSource>();

        if (randomHitCount)
        {
            maxHitCount = Random.Range(1, maxHitCount + 1); // Assicura almeno 1 hit
        }

        _destroyStepsPercent = 1f / maxHitCount;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<MoveBullet>()) return;

        maxHitCount--;

        // Effetto trasparenza più fluido
        Color newColor = _material.color;
        newColor.a = Mathf.Lerp(newColor.a, 0, _destroyStepsPercent);
        _material.color = newColor;

        Debug.Log($"Colpito! Alpha ridotto a: {_material.color.a}");

        if (maxHitCount <= 0)
        {
            _gameManager?.DidDestroyWall();
            ScoreManager.Instance?.AddScore(10); // Aggiunge punti al punteggio

            if (_audioSource && _audioSource.clip)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                Invoke(nameof(DestroyMe), _audioSource.clip.length); // Aspetta la fine dell'audio
            }
            else
            {
                DestroyMe();
            }

            Destroy(this); // Evita altri colpi
        }
    }

    private void DestroyMe()
    {
        _gameManager = null;
        Destroy(gameObject);
    }
}
