using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationRouter : IRouter
{
    private Dictionary<AbstractEntity, AnimationData> _entitiesByDatas = new();
        
    public void Init()
    {
        AttackController.Instance.Attacked += OnAttacked;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnAttacked(AbstractEntity obj)
    {
        if (obj is PlayerView)
        {
            Transform armWithGun = obj.ArmWithGun.transform;

            if (_entitiesByDatas.ContainsKey(obj) == false)
            {
                AnimationData data = new AnimationData(armWithGun.localPosition, armWithGun.localRotation);

                data.TimeLessZero += () => CoroutineController.Instance.RunCoroutine(PlayEndShotAnimation(obj, data));
                data.StartedAfterLongTime += () => CoroutineController.Instance.RunCoroutine(PlayStartShotAnimation(obj, data));
                data.UpdatedTime += () => CoroutineController.Instance.RunCoroutine(PlayShotAnimation(obj, data));
                
                data.ResetTime();
                
                _entitiesByDatas.Add(obj, data);
            }
            else
            {
                _entitiesByDatas[obj].ResetTime();
            }
        }
    }

    private IEnumerator PlayStartShotAnimation(AbstractEntity obj, AnimationData data)
    {
        Debug.Log("START SHOOT!");
        if (obj == null || obj.ArmWithGun == null)
        {
            yield break;
        }
        Transform armWithGun = obj.ArmWithGun.transform;
    
        // Запоминаем исходные позицию и вращение

        // Первая анимация - перемещение и вращение к начальной точке отдачи
        yield return DOTween.Sequence()
            .Join(armWithGun.DOLocalRotate(new Vector3(260, data.OriginalRotation.eulerAngles.y, data.OriginalRotation.eulerAngles.z), 0.2f))
            .Join(armWithGun.DOLocalMove(new Vector3(-0.0006f, 0.0073f, 0.0076f), 0.2f)).WaitForCompletion();
    }    
    
    private IEnumerator PlayEndShotAnimation(AbstractEntity obj, AnimationData data)
    {
        Debug.Log("END SHOOT!");
        if (obj == null || obj.ArmWithGun == null)
        {
            yield break;
        }
        Transform armWithGun = obj.ArmWithGun.transform;
    
        // Запоминаем исходные позицию и вращение
        
        yield return DOTween.Sequence()
            .Join(armWithGun.DOLocalRotate(data.OriginalRotation.eulerAngles, 0.2f))
            .Join(armWithGun.DOLocalMove(data.OriginalPosition, 0.2f))
            .WaitForCompletion();
    }

    private IEnumerator PlayShotAnimation(AbstractEntity obj, AnimationData data)
    {
        if (obj == null || obj.ArmWithGun == null)
        {
            yield break;
        }
        Debug.Log("SHOOTING!!!");

        Transform armWithGun = obj.ArmWithGun.transform;
    
        // Запоминаем исходные позицию и вращение
    
        // Вторая анимация - поднятие руки (увеличение вращения по X)
        yield return armWithGun.DOLocalRotate(new Vector3(255, data.OriginalRotation.eulerAngles.y, data.OriginalRotation.eulerAngles.z), 0.2f)
            .WaitForCompletion();
    
        yield return armWithGun.DOLocalRotate(new Vector3(250, data.OriginalRotation.eulerAngles.y, data.OriginalRotation.eulerAngles.z), 0.15f)
            .WaitForCompletion();

        // Возврат к исходному положению
    }

    private void OnUpdate()
    {
        foreach (var item in _entitiesByDatas)
        {
            item.Value.DecreasedTime();
        }
    }

    public void Exit()
    {
        
    }
}