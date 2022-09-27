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
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {

        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            if((eventData.position.y - _startingPos.y) >= 500)
            {
                _cardImage.color = colors[0];
            }
            else
            {
                _cardImage.color = colors[1];
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
