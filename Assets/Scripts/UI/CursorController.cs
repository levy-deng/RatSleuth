using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The HUD Controller.")]
    private GameObject m_HUD;

    [SerializeField]
    [Tooltip("Cursors")]
    public Texture2D walk;
    public Texture2D magnify;
    public Texture2D magnify2;
    public Texture2D talk;
    public Texture2D talk2;
    public Texture2D item;
    private bool stop;
    private bool hover;
    private bool isTalk;
    #endregion

    #region Methods
    private void Start() {
        Cursor.SetCursor(walk, Vector2.zero, CursorMode.ForceSoftware);
        stop = false;
        hover = false;
    }

    private void Update() {
        int mode = m_HUD.GetComponent<HUDController>().GetMode();
        if (!stop) {
            switch (mode) {
                case 0:
                    Cursor.SetCursor(walk, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case 1:
                    if (hover)
                        Cursor.SetCursor(magnify2, Vector2.zero, CursorMode.ForceSoftware);
                    else
                        Cursor.SetCursor(magnify, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case 2:
                    if (hover)
                        Cursor.SetCursor(talk2, Vector2.zero, CursorMode.ForceSoftware);
                    else
                        Cursor.SetCursor(talk, Vector2.zero, CursorMode.ForceSoftware);
                    break;
                case 3:
                    Cursor.SetCursor(item, Vector2.zero, CursorMode.ForceSoftware);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            stop = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }

    }

    public void changeHover(bool b) {
        this.hover = b;
    }

    public IEnumerator trackCursor(GameObject item) {
        while (true) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                yield return null;
            }
            Vector3 mousePosition = Input.mousePosition;
            Vector3 newPos = mousePosition;
            newPos.x -= 10;
            newPos.y -= 10;
            item.transform.position = newPos;
        }
    }
    #endregion
}
