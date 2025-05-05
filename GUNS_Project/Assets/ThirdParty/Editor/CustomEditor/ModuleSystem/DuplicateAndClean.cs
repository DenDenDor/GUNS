using UnityEngine;
using UnityEditor;

public class DuplicateAndClean : EditorWindow
{
    [MenuItem("Tools/Duplicate And Clean")]
    public static void ShowWindow()
    {
        GetWindow<DuplicateAndClean>("Duplicate And Clean");
    }

    void OnGUI()
    {
        GUILayout.Label("Duplicate Selected Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Duplicate And Clean"))
        {
            DuplicateSelectedObjects();
        }
    }

    void DuplicateSelectedObjects()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected!");
            return;
        }

        // Запоминаем текущее выделение, чтобы восстановить его позже
        GameObject[] originalSelection = Selection.gameObjects;
        GameObject[] newSelection = new GameObject[originalSelection.Length];

        for (int i = 0; i < originalSelection.Length; i++)
        {
            GameObject original = originalSelection[i];
            
            // Создаем дубликат
            GameObject duplicate = Instantiate(original);
            duplicate.name = "!!" + original.name;

            // Разрываем связь с префабом (если нужно)
            if (PrefabUtility.IsPartOfAnyPrefab(duplicate))
            {
                PrefabUtility.UnpackPrefabInstance(duplicate, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            }

            // Удаляем всех детей (корректный способ)
            while (duplicate.transform.childCount > 0)
            {
                DestroyImmediate(duplicate.transform.GetChild(0).gameObject);
            }

            // Сохраняем трансформы оригинала
            duplicate.transform.SetParent(original.transform.parent);
            duplicate.transform.SetPositionAndRotation(original.transform.position, original.transform.rotation);
            duplicate.transform.localScale = original.transform.localScale;

            // Добавляем в массив для нового выделения
            newSelection[i] = duplicate;
        }

        // Выделяем все созданные дубликаты
        Selection.objects = newSelection;
    }
}