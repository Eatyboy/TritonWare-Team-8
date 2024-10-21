using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] int currentOption = 0;
    [SerializeField] Transform[] options;
    [SerializeField] AudioClip changeAudio;
    [SerializeField] AudioClip selectAudio;

    private void OnEnable()
    {
        currentOption = 0;
        ChangeOption(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeOption(-1);
		}
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeOption(1);
		}
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectOption();
		}
    }

    public void ChangeOption(int n)
    {
        currentOption += n;
        if (currentOption == -1)
        {
            currentOption = options.Length - 1;
		}
        else if (currentOption == options.Length)
        {
            currentOption = 0;
		}
        transform.position = new Vector2(transform.position.x, options[currentOption].transform.position.y);
	}

    public void SelectOption()
    {
        options[currentOption].GetComponent<Button>().onClick.Invoke();
	}
}