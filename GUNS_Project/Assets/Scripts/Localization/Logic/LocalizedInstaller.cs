using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Localization
{

    public class LocalizedInstaller : MonoBehaviour
    {
        [SerializeField] private FactoryLocalizedObject _factoryLocalizedObject;
        [SerializeField] private List<LanguageType> _languageTypes;


        private List<DisplayView> _views = new();

        private IEnumerable<ILocalizedObject<ILocalizedData>> _localizedObjects;

        private LanguageType _currentType = LanguageType.En;


        public static LocalizedInstaller Instance { get; private set; }

        private void Awake()
        {
            _localizedObjects = _factoryLocalizedObject.Create();


            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        { 
            //LOAD 
            //_currentType = SDKMediator.Instance.GetLanguage();
        }

        private void UpdateLocalization(DisplayView view)
        {
            ILocalizedObject<ILocalizedData> data =
                _localizedObjects.FirstOrDefault(text => text.Key == view.Key);

            if (data != null)
            {
                ILocalizedData localizedData =
                    data.ObjectByLanguage.FirstOrDefault(model => model.LanguageType == _currentType);

                if (localizedData != null)
                {
                    view.Accept(new DisplayLocalizationViewVisitor(localizedData));
                }
                else
                {
                    Debug.LogError(data.Key + " NOT FOUND KEY");
                }
            }
        }

        public void AddView(DisplayView view)
        {
            _views.Add(view);

            UpdateLocalization(view);
        }

        public void RemoveView(DisplayView view)
        {
            _views.Remove(view);
        }

        public void ChangeLanguage()
        {
            int index = _languageTypes.IndexOf(_currentType);

            if (index >= _languageTypes.Count - 1)
                _currentType = _languageTypes.First();
            else
                _currentType = _languageTypes[index + 1];

            //SAVE
            //SDKMediator.Instance.SaveLanguage(_currentType);

            foreach (var view in _views)
            {
                UpdateLocalization(view);
            }
        }
    }
}