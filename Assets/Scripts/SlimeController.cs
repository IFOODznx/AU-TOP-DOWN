using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class SlimeController : MonoBehaviour
{
    // Variável para controlar a velocidade do slime
    public float velocidade = 3.5f;
    private Rigidbody2D rb;
    private Vector2 direcao;
    public DetectarCOntroller detectarArea;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtém o componente Rigidbody2D para controlar o movimento do slime
        rb = GetComponent<Rigidbody2D>();
        // Obtém o componente SpriteRenderer para controlar a aparência do slime
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calcula a direção do movimento com base na entrada do jogador
        direcao = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    // Move o slime na direção calculada
    private void FixedUpdate()
    {
        // Verifica se há objetos detectados na área de detecção
        if (detectarArea.detectarObj.Count > 0)
        {
            // Move o slime em direção ao primeiro objeto detectado
            direcao = (detectarArea.detectarObj[0].transform.position - transform.position).normalized;
            // Move o slime usando o Rigidbody2D para garantir uma movimentação suave
            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);

            // Verifica a direção do movimento para ajustar a orientação do sprite
            if(direcao.x > 0)
            {
                // Se o slime estiver se movendo para a direita, mantém o sprite normal
                spriteRenderer.flipX = false; // Virar para a direita
            }
            // Verifica se o slime está se movendo para a esquerda
            else if(direcao.x < 0)
            {
                // Se o slime estiver se movendo para a esquerda, inverte o sprite
                spriteRenderer.flipX = true; // Virar para a esquerda
            }
        }
    }

    
}
