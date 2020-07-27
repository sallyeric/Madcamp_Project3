using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StuffEvent : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject ChatBox;
    TextMeshPro textMeshPro;

    void Start()
    {
        //ChatBox = GameObject.FindWithTag("ChatBox");
        textMeshPro = ChatBox.GetComponent<TextMeshPro>();
        ChatBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bench")
        {
            textMeshPro.text = "앉아서 좀 쉬고 싶군..";
            ChatBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bench")
            ChatBox.SetActive(false);
        
    }
}
