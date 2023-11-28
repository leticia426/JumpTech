using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CenaEstado
{
    public List<ObjetoEstado> objetos;
}

[System.Serializable]
public class ObjetoEstado
{
    public string nomeObjeto;
    public Vector3 posicao;
    public Quaternion rotacao;
    // Adicione mais informações conforme necessário
}

public class SistemaCheckpoint : MonoBehaviour
{
    private CenaEstado estadoAtual;

    void SalvarEstado()
    {
        estadoAtual = new CenaEstado();
        estadoAtual.objetos = new List<ObjetoEstado>();

        // Salva a posição do jogador
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            ObjetoEstado jogadorEstado = new ObjetoEstado
            {
                nomeObjeto = jogador.name,
                posicao = jogador.transform.position,
                rotacao = jogador.transform.rotation
                // Adicione mais informações conforme necessário
            };

            estadoAtual.objetos.Add(jogadorEstado);
        }

        // Salva a posição de todas as moedas
        GameObject[] moedas = GameObject.FindGameObjectsWithTag("Moeda");
        foreach (var moeda in moedas)
        {
            ObjetoEstado moedaEstado = new ObjetoEstado
            {
                nomeObjeto = moeda.name,
                posicao = moeda.transform.position,
                rotacao = moeda.transform.rotation
                // Adicione mais informações conforme necessário
            };

            estadoAtual.objetos.Add(moedaEstado);
        }
    }

    void CarregarEstado()
    {
        if (estadoAtual != null)
        {
            foreach (var objEstado in estadoAtual.objetos)
            {
                GameObject obj = GameObject.Find(objEstado.nomeObjeto);

                if (obj != null)
                {
                    obj.transform.position = objEstado.posicao;
                    obj.transform.rotation = objEstado.rotacao;
                    // Adicione mais informações conforme necessário
                }
            }
        }
    }

    // ... Outros métodos e funcionalidades do sistema de checkpoint
}