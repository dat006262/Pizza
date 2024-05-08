using BulletHell;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PizzaGame : MonoBehaviour
{
    #region KnifeRotate
    [Foldout("KnifeRotate", true)]
    public SingleRotate knifeRotate;
    public Transform knifeHolder;
    [Range(1, 360)] public float speed;
    public Image pizzaClockWise;
    public Image pizzaNotClockWise;
    public TextMeshProUGUI textMeshProUGUI;
    Tween tween;
    #endregion

    #region KnifeMove
    [Foldout("KnifeMove", true)]
    public MoveKnife moveKnife;
    public Transform knifeMesh;
    public bool canCut;
    #endregion

    [SerializeField] private int goc_hien_tai => 360 - Mathf.RoundToInt(knifeHolder.transform.rotation.eulerAngles.z);

    private void Start()
    {
        canCut = false;
        knifeRotate = new SingleRotate(knifeHolder, speed, true);

        moveKnife = new MoveKnife(knifeMesh);

        moveKnife.DoMoveTo(knifeHolder.transform.position + Vector3.forward * -5, new Vector3(0, -90, -180), 1f, () => { canCut = true; moveKnife.transform.SetParent(knifeHolder); });
    }
    private void Update()
    {
        if (!canCut) { return; }
        knifeRotate.UpdateRotate(Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            if (tween != null)
            {
                tween.Kill();
                tween = null;
            }

            Render();
            Vector3 direcMove = CalcultateDirecMove();


            knifeRotate.SetStopOrRun(
                () =>
                {

                    ReStart();
                },
                () =>
                {
                    //NativeManager.Instance.SendDataScore(goc_hien_tai.ToString());
                    SoundManager.Instance.PlaySfxOverride(GlobalSetting.Instance.soundDataSO.GetSfx(SoundDataSO._SoundEnum.KnifeDown));
                    tween = pizzaClockWise.DOFade(0, 2f);
                    pizzaClockWise.transform.DOMove(pizzaClockWise.transform.position + direcMove, 1);
                    pizzaNotClockWise.transform.DOMove(pizzaClockWise.transform.position + -direcMove, 1);
                    knifeHolder.DOMove(knifeHolder.transform.position + 2 * knifeHolder.up, 2f);
                });

        }
    }

    private Vector3 CalcultateDirecMove()
    {
        Vector3 direcMove;
        if (knifeHolder.transform.up.x > 0)
        {
            direcMove = -(knifeHolder.transform.up + Vector3.up).normalized;
        }
        else
        {
            direcMove = (knifeHolder.transform.up + Vector3.up).normalized;
        }
        return direcMove;
    }
    private void Render()
    {
        pizzaClockWise.fillAmount = RotationZToFillAmount(360 - goc_hien_tai);
        pizzaNotClockWise.fillAmount = 1 - pizzaClockWise.fillAmount;
        textMeshProUGUI.text = $"Goc la {goc_hien_tai} do";
    }
    private float RotationZToFillAmount(float rotateZ)
    {
        rotateZ = rotateZ / 360.0f;
        return rotateZ;
    }

    public void ChangScene()
    {
        GlobalSetting.LoadScene(SceneEnum.GAMEPLAY);
    }
    public void ReStart()
    {
        textMeshProUGUI.text = $"Goc la 0 do";
        canCut = false;
        DOTween.PauseAll();
        knifeHolder.rotation = Quaternion.identity;
        knifeHolder.transform.position = Vector3.zero;
        knifeMesh.SetParent(knifeHolder.parent.transform);
        knifeMesh.transform.localPosition = new Vector3(1.5f, 0, 0);
        knifeMesh.transform.rotation = Quaternion.identity;
        pizzaClockWise.color = Color.white;
        pizzaClockWise.transform.localPosition = Vector3.zero;
        pizzaNotClockWise.transform.localPosition = Vector3.zero;

        moveKnife.DoMoveTo(knifeHolder.transform.position + Vector3.forward * -5, new Vector3(0, -90, -180), 1f, () => { canCut = true; moveKnife.transform.SetParent(knifeHolder); });

    }
}
