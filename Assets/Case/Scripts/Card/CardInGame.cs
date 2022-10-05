using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Case.Characters;
using Case.Energy;
using Case.BorderControl;
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

        private GameObject _ghostCharacter;

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
            CameraControl.Instance.ChangePerspective(0,0);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {


            if ((eventData.position.y - _startingPos.y) >= Screen.height * .25f)
            {
                _cardImage.color = colors[0];
                transform.position = Vector3.up * -1500;

                //Ghost character
                if (_ghostCharacter != null)
                {
                    _ghostCharacter.transform.position = GetTouchPosition();
                }
                else if(_ghostCharacter == null)
                {
                    GameObject obj =  Instantiate(_characterProperties.Character.transform.GetChild(0).transform.gameObject, GetTouchPosition(), Quaternion.identity);

                    Material mat = obj.GetComponentInChildren<SkinnedMeshRenderer>().material;
                    mat.color = new Color32(255,255,255, 25);

                    _ghostCharacter = obj;
                }
            }
            else
            {
                _cardImage.color = colors[1];
                transform.position = eventData.position;
                Destroy(_ghostCharacter);
            }


            if ((eventData.position.y - _startingPos.y) >= Screen.height * .38f)
            {
                BorderManager.Instance.BorderCondition(true);
            }
            else
            {
                BorderManager.Instance.BorderCondition(false);
            }

            CameraControl.Instance.ChangePerspective((eventData.position.x - Screen.width / 2 ) / Screen.width, eventData.position.y / Screen.height);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startingPos;


            if (Input.GetMouseButtonUp(0) && IsGround() && EnergyManager.Instance.energy >= _characterProperties.Energy)
            {
                GameObject obj = Instantiate(_characterProperties.Character, GetTouchPosition(), Quaternion.identity);
                obj.GetComponent<Character>().Initialize("Friend");

                EnergyManager.Instance.SpendEnergy(_characterProperties.Energy);

               
            }

            Destroy(_ghostCharacter);
            _cardImage.color = new Color32(255, 255, 255, 255);
            BorderManager.Instance.BorderCondition(false);
            CameraControl.Instance.ChangePerspective(0, 0);
        }

        private Vector3 GetTouchPosition()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    return hit.point;
                }
            }
            return Vector3.up * -500;
        }

        private bool IsGround()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
