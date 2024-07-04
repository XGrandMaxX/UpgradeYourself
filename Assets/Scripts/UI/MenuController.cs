using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Image _questBookPanel;
        [SerializeField] private Image _userProfilePanel;


        public void EnablePanel(Image panel)
        {
            if (panel.gameObject.activeInHierarchy)
                return;
            
            DisableAllPanels();
            
            panel.gameObject.SetActive(true);
        }
        
        private void DisableAllPanels()
        {
            Image[] panels = { _questBookPanel, _userProfilePanel };
            
            foreach (var panel in panels)
                panel.gameObject.SetActive(false);
        }
    }
}
