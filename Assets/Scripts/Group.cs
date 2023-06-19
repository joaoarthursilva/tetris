using UnityEngine;
using UnityEngine.SceneManagement;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    private float _lastFall;
    private bool _inputGoLeft;
    private bool _inputGoRight;
    private bool _inputRotate;
    private bool _inputGoDown;


    private void Start() // quando spawna uma peça
    {
        _inputGoLeft = false;
        _inputGoRight = false;
        _inputRotate = false;
        _inputGoDown = false;
        _lastFall = 0;
        if (!IsValidGridPos()) // se a peça spawnar em uma posição inválida (em outra peça), game over
        {
            GameOver();
            Destroy(gameObject);
        }
    }

    private void GameOver()
    {
        Invoke(nameof(Reload), 1f);
    }

    private void Reload()
    {
        SceneManager.LoadScene("Tetris");
    }

    private void Update()
    {
        ManageInput();
    }


    private void ManageInput()
    {
        // Left
        if (_inputGoLeft)
        {
            _inputGoLeft = false;
            // Move pra esquerda
            transform.position += new Vector3(-1, 0, 0);

            if (IsValidGridPos())
                UpdateGrid();
            else
                transform.position += new Vector3(1, 0, 0);
        }

        // Right
        else if (_inputGoRight)
        {
            _inputGoRight = false;
            // Move pra direita
            transform.position += new Vector3(1, 0, 0);

            if (IsValidGridPos())
                UpdateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (_inputRotate)
        {
            _inputRotate = false;
            transform.Rotate(0, 0, -90);

            if (IsValidGridPos())
                UpdateGrid();
            else
                transform.Rotate(0, 0, 90);
        }

        // Down or Fall
        else if (_inputGoDown ||
                 Time.time - _lastFall >= 1)
        {
            _inputGoDown = false;
            // Move pra baixo
            transform.position += new Vector3(0, -1, 0);

            if (IsValidGridPos()) // pode descer
            {
                UpdateGrid();
            }
            else // se colidir numa peça
            {
                transform.position += new Vector3(0, 1, 0);

                // Se esse movimento completar uma linha, limpa a linha
                Map.DeleteFullRows();

                // Spawna a proxima peça
                FindObjectOfType<Spawner>().SpawnNext();

                // desabilita o script
                enabled = false;
            }

            _lastFall = Time.time;
        }
    }

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            var posicaoArredondada = Map.RoundVec2(child.position);

            if (!Map.IsInsideBorder(posicaoArredondada))
                return false;

            if (Map.Grid[(int) posicaoArredondada.x, (int) posicaoArredondada.y] != null &&
                Map.Grid[(int) posicaoArredondada.x, (int) posicaoArredondada.y].parent != transform)
                return false;
        }

        return true;
    }

    private void UpdateGrid()
    {
        // Remove old children from grid
        for (var y = 0; y < Map.H; ++y)
        {
            for (var x = 0; x < Map.W; ++x)
            {
                if (Map.Grid[x, y] != null)
                {
                    if (Map.Grid[x, y].parent == transform)
                    {
                        Map.Grid[x, y] = null;
                    }
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in transform)
        {
            var posicaoArredondada = Map.RoundVec2(child.position);
            Map.Grid[(int) posicaoArredondada.x, (int) posicaoArredondada.y] = child;
        }
    }

    public void InputGoLeft()
    {
        _inputGoLeft = true;
    }

    public void InputGoRight()
    {
        _inputGoRight = true;
    }

    public void InputRotate()
    {
        _inputRotate = true;
    }

    public void InputGoDown()
    {
        _inputGoDown = true;
    }
}