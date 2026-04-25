using System.Collections;
using UnityEngine;

public class ObjetoTransparente : MonoBehaviour
{
    // Valor de transparência (0 = totalmente transparente, 1 = totalmente opaco)
    [Range(0f, 1f)]
    [SerializeField]
    private float valorTransparenteColisao = 0.5f; // Valor de transparência quando colidindo com o jogador
    [SerializeField]
    private float tempoTransicao = .4f; // Tempo para transição de transparência
    private SpriteRenderer spriteRenderer; // Referência ao componente SpriteRenderer do objeto

    // Método chamado no início do jogo
    void Awake()
    {
        // Obtém o componente SpriteRenderer do objeto
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Método chamado quando outro objeto entra no trigger deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            // Se o objeto que entrou no trigger for o jogador, torna o objeto um pouco transparente
            StartCoroutine(EsconderArvore(spriteRenderer, tempoTransicao, spriteRenderer.color.a, valorTransparenteColisao));
            // Também torna o jogador transparente
            SpriteRenderer playerRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                StartCoroutine(EsconderArvore(playerRenderer, tempoTransicao, playerRenderer.color.a, valorTransparenteColisao));
            }
        }
    }

    // Método chamado quando outro objeto sai do trigger deste objeto
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            // Se o objeto que saiu do trigger for o jogador, torna o objeto totalmente opaco novamente
            StartCoroutine(EsconderArvore(spriteRenderer, tempoTransicao, spriteRenderer.color.a, 1.0f));
            // Também restaura a opacidade do jogador
            SpriteRenderer playerRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                StartCoroutine(EsconderArvore(playerRenderer, tempoTransicao, playerRenderer.color.a, 1.0f));
            }
        }
    }

    // Método chamado quando outro objeto sai do trigger deste objeto
    private IEnumerator EsconderArvore(SpriteRenderer spriteTransparente, float tempoEsconder, float valorInicial, float targetTransparencia)
    {
        // Variável para controlar o tempo decorrido
        float tempoDecorrido = 0f;
        // Loop para realizar a transição de transparência ao longo do tempo especificado
        while (tempoDecorrido < tempoEsconder)
        {
            // Calcula a nova transparência usando interpolação linear (Lerp) e aplica ao SpriteRenderer
            tempoDecorrido += Time.deltaTime;
            // Calcula a nova transparência usando interpolação linear (Lerp) e aplica ao SpriteRenderer
            float novaTransparencia = Mathf.Lerp(valorInicial, targetTransparencia, tempoDecorrido / tempoEsconder);
            // Aplica a nova transparência ao SpriteRenderer, mantendo as cores RGB originais
            spriteTransparente.color = new Color(spriteTransparente.color.r, spriteTransparente.color.g, spriteTransparente.color.b, novaTransparencia);
            yield return null; // Espera até o próximo frame
        }
    }
}
