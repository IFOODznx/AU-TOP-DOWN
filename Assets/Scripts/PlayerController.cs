using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    private Rigidbody2D rb;
    private Animator animator;
    public float velocidade;
    private Vector2 direcao;
    private float velocidadeInicial;
    public float aceleracaoVelocidade;
    private bool isAtaque = false;


    // Método chamado no início do jogo
    void Start()
    {
        // Obtém os componentes Rigidbody2D e Animator do jogador
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Armazena a velocidade inicial do jogador para referência futura
        velocidadeInicial = velocidade;
    }

    // Método chamado a cada frame para capturar a entrada do jogador
    void Update()
    {
        //Flip();
        AceleracaoPlayer();
        onAtaque();
    }

    // Método chamado a cada intervalo fixo para aplicar o movimento do jogador
    void FixedUpdate()
    {
        // Captura a entrada do jogador para movimento horizontal e vertical
        direcao = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // Atualiza o parâmetro de movimento do animator com base na direção capturada
        if (direcao.sqrMagnitude > 0.1)
        {
            MovimentoPlayer();
            // Atualiza os parâmetros de direção no animator para controlar as animações de movimento
            animator.SetFloat("AxisX", direcao.x);
            animator.SetFloat("AxisY", direcao.y);
            // Se o jogador estiver se movendo, define o parâmetro "Movimento" para 1
            animator.SetInteger("Movimento", 1);
        }else
        {
            // Se o jogador não estiver se movendo, define o parâmetro "Movimento" para 0
            animator.SetInteger("Movimento", 0);
        }

        // Verifica se o jogador está atacando e atualiza a animação de ataque conforme necessário
        if(isAtaque)
        {
            // Se o jogador estiver atacando, define o parâmetro "Movimento" para 0 para garantir que a animação de ataque seja exibida corretamente
            animator.SetInteger("Movimento", 2);
        }
    }

    // Método para mover o jogador com base na direção capturada
    void MovimentoPlayer()
    {
        // Move o jogador na direção capturada multiplicada pela velocidade e pelo tempo fixo
        rb.MovePosition(rb.position + direcao.normalized * velocidade * Time.fixedDeltaTime);
    }

    // Método para virar o jogador na direção do movimento
    void Flip()
    {
        if(direcao.x > 0)
        {
            // Se o jogador estiver se movendo para a direita, define a rotação para 0 graus
            transform.eulerAngles = new Vector3(0f, 0f);
        }
        else if(direcao.x < 0)
        {
            // Se o jogador estiver se movendo para a esquerda, define a rotação para 180 graus
            transform.eulerAngles = new Vector3(0f, 180f);
        }
    }

    // Método para acelerar o jogador quando a tecla Shift estiver pressionada
    void AceleracaoPlayer()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Se a tecla Shift estiver pressionada, aumenta a velocidade do jogador com base na aceleração
            velocidade += aceleracaoVelocidade;
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Se a tecla Shift for liberada, redefine a velocidade do jogador para a velocidade inicial
            velocidade = velocidadeInicial;
        }
    }

    void onAtaque()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Se a tecla de ataque (espaço) for pressionada, define o estado de ataque para verdadeiro
            isAtaque = true;
            velocidade = 0; // Define a velocidade do jogador para 0 durante o ataque
        }
        else if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            // Se a tecla de ataque for liberada, redefine o estado de ataque para falso
            isAtaque = false;
            velocidade = velocidadeInicial; // Redefine a velocidade do jogador para a velocidade inicial
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Inimigo"))
        {
            // Se o jogador colidir com um objeto com a tag "Inimigo", destrói o jogador
            animator.SetTrigger("Morte"); // Aciona a animação de morte
            GetComponent<Rigidbody2D>().simulated = false; // Desativa a simulação física do jogador
            Destroy(gameObject, 0.3f); // Destroi o jogador após um curto período para permitir que a animação de morte seja exibida
        }
    }
    
}
