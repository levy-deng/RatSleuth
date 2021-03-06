using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The conversation to be had on interaction.")]
    private Convo[] m_Conversation;

    [SerializeField]
    [Tooltip("the GameObject for controlling inventory serialization")]
    private GameObject m_Inventory;

    [SerializeField]
    [Tooltip("The object to display the conversation in.")]
    private GameObject m_DiaBox;

    [SerializeField]
    [Tooltip("A HUD Controller object to check modes.")]
    private GameObject m_HUD;

    public Animator animator;

    [SerializeField]
    [Tooltip("Whether or not to deactivate this object after its conversation.")]
    private bool m_Deactivate;
    #endregion

    #region Cached Components
    private Image p_Portrait;
    private Text p_Name;
    private Text p_Text;

    private Inventory p_Inventory;

    private HUDController p_HUDController;

    private string p_ObjectName;
    #endregion

    #region Private Variables
    private int p_Index;
    #endregion

    #region Initialization
    private void Awake() {
        p_Portrait = m_DiaBox.transform.GetChild(0).gameObject.GetComponent<Image>();
        p_Name = m_DiaBox.transform.GetChild(1).gameObject.GetComponent<Text>();
        p_Text = m_DiaBox.transform.GetChild(2).gameObject.GetComponent<Text>();
        p_Index = -1;
        p_Inventory = m_Inventory.GetComponent<Inventory>();

        p_HUDController = m_HUD.GetComponent<HUDController>();

        p_ObjectName = "";
    }
    #endregion

    #region Update Methods
    private void Update() {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) {
            p_Index++;
        }

        if (p_Index >= 0 && p_Index < this.m_Conversation.Length && p_HUDController.ModeInt == 1) {
            p_Portrait.sprite = Liner(p_Index).portrait;
            p_Name.text = Liner(p_Index).name;
            p_Name.color = new Color(0, 0, 0, 1);
            p_Text.text = Liner(p_Index).text;
            p_Text.color = new Color(0, 0, 0, 1);
        } else if (p_HUDController.ModeInt != 1) {
            p_Index = -1;
            m_DiaBox.SetActive(false);
        } else if (p_Index >= this.m_Conversation.Length) {
            p_Index = -1;
            m_DiaBox.SetActive(false);
            if (p_ObjectName == this.gameObject.name && m_Deactivate) {
                gameObject.SetActive(false);
            }
        } else {
            p_Index = -1;
        }

        if (IsTalking() == false) {
            p_Index = -1;
            animator.SetBool("isPicking", false);
        }
        else
        {
            animator.SetBool("isPicking", true);
        }
    }
    #endregion

    #region Dialogue Methods
    private Convo Liner(int i) {
        return m_Conversation[i];
    }
    #endregion

    #region Check Methods
    public bool IsTalking() {
        return m_DiaBox.activeInHierarchy;
    }

    public void ChangeState() {
        m_DiaBox.SetActive(true);
        p_ObjectName = this.gameObject.name;
    }
    #endregion

    [System.Serializable]
    private class Convo {
        [SerializeField]
        public Sprite portrait;

        [SerializeField]
        public string name;

        [SerializeField]
        public string text;
    }
}
