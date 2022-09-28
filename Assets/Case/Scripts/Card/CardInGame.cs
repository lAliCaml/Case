using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Case.Characters;
using Case.Energy;
using UnityEngine.UI;

namespace Case.Card
{
    public class CardInGame : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Vector2 _startingPos;

        [SerializeField] private CharacterProperties _characterProperties;
        [SerializeField] private Image _cardImage;

        [SerializeField] private Text text_Energy;

        private Color32[] colors;


        RaycastHit hit;
        private void Start()
        {
            _startingPos = transform.position;

            colors = new Color32[2];
            colors = new Color32[]
            {
                new Color32(0, 0, 0, 0),
                new Color32(255, 255, 255, 255)
            };

            text_Energy.text = _characterProperties.Energy.ToString();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {

        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            

            if((eventData.position.y - _startingPos.y) >= 500)
            {
                _cardImage.color = colors[0];
                transform.position = Vector3.up * -1500;
            }
            else
            {
                _cardImage.color = colors[1];
                transform.position = eventData.position;
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startingPos;

            if (Input.GetMouseButtonUp(0) && EnergyManager.Instance.energy >= _characterProperties.Energy)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Ground")
                    {
                        GameObject obj = Instantiate(_characterProperties.Character, hit.point, Quaternion.identity);
                        obj.GetComponent<Character>().Initialize("Friend");

                        EnergyManager.Instance.SpendEnergy(_characterProperties.Energy);
                    }
                }
            }
            _cardImage.color = new Color32(255, 255, 255, 255);
        }
    }
}
