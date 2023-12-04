﻿using DataStore.Quiz;
using Notero.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Notero.QuizConnector
{
    public class QuizConnectorController : MonoSingleton<QuizConnectorController>
    {
        [SerializeField]
        private Transform m_Container;

        [SerializeField]
        private List<SlotQuiz> m_SlotQuizList;

        public bool IsQuizLoaded
        {
            get => m_QuizControllerInterface is { IsQuizLoaded: true };
        }

        public GameObject CurrentUI { get; private set; }

        public UnityEvent OnNextStateReceive;
        public UnityEvent<byte[]> OnCustomDataReceive;

        public UnityEvent<string> OnStudentSubmit;

        private IQuizController m_QuizControllerInterface;

        private Func<string, string, Texture> m_GenerateTextureLogicDefault { get; set; } = (path, rootDir) =>
        {
            path = Path.Combine(rootDir, path);

            var fileByte = File.ReadAllBytes(path);
            var texture = new Texture2D(1, 1);

            texture.LoadImage(fileByte);

            return texture;
        };

        public void Init(QuizStore quizStore, string keyName)
        {
            var slotQuiz = GetSlotQuizByKeyName(keyName);

            if(slotQuiz.ControllerGameObject.activeInHierarchy)
            {
                m_QuizControllerInterface = slotQuiz.ControllerGameObject.GetComponent<IQuizController>();
            }
            else
            {
                var gameObj = Instantiate(slotQuiz.ControllerGameObject);

                gameObj.name = keyName;

                m_QuizControllerInterface = gameObj.GetComponent<IQuizController>();
            }

            m_QuizControllerInterface.Init(m_Container, quizStore, m_GenerateTextureLogicDefault);

            m_QuizControllerInterface.SetChapterIndex(-1);
            m_QuizControllerInterface.SetRootDirectory("");
            m_QuizControllerInterface.SetChapter("");
            m_QuizControllerInterface.SetMission("");

            SubscribeEvent();
        }

        public void SetChapterIndex(int chapterIndex)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetChapterIndex(chapterIndex);
        }

        public void SetRootDirectory(string rootDirectory)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetRootDirectory(rootDirectory);
        }

        public void SetChapter(string chapter)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetChapter(chapter);
        }

        public void SetMission(string mission)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetMission(mission);
        }

        private void SubscribeEvent()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.OnNextStateReceive = OnNextStateReceive;
            m_QuizControllerInterface.OnCustomDataReceive = OnCustomDataReceive;

            m_QuizControllerInterface.OnStudentSubmit = OnStudentSubmit;
        }

        public void OnStudentAnswerReceive(string stationId, string answer, int answerStudentAmount, int studentAmount)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.OnStudentAnswerReceive(stationId, answer, answerStudentAmount, studentAmount);
        }

        public void OnCustomDataMessageReceive(byte[] data)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.OnCustomDataMessageReceive(data);
        }

        public void SetGenerateTextureLogic(Func<string, string, Texture> logic)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetGenerateTextureLogic(logic);
        }

        public void LoadQuizToQuizStore(string jsonContent)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.LoadQuizToQuizStore(jsonContent);
        }

        public void SetCurrentUI(GameObject current) => CurrentUI = current;

        public void DestroyCurrentUI() => Destroy(CurrentUI.gameObject);

        public void SpawnInstructorCountInStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnInstructorCountInStateUI();
        }

        public void DestroyInstructorCountInStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyInstructorCountInStateUI();
        }

        public void AddInstructorFinishCountInStateEventListener(UnityAction action)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.AddInstructorFinishCountInStateEventListener(action);
        }

        public void RemoveInstructorFinishCountInStateEventListener(UnityAction action)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.RemoveInstructorFinishCountInStateEventListener(action);
        }

        public void SpawnInstructorQuestionStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnInstructorQuestionStateUI();
        }

        public void DestroyInstructorQuestionStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyInstructorQuestionStateUI();
        }

        public void SetFullScreen(bool isFull)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SetFullScreen(isFull);
        }

        public void SpawnInstructorPreResultStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnInstructorPreResultStateUI();
        }

        public void DestroyInstructorPreResultStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyInstructorPreResultStateUI();
        }

        public void SpawnInstructorEndStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnInstructorEndStateUI();
        }

        public void DestroyInstructorEndStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyInstructorEndStateUI();
        }

        public void SpawnInstructorResultStateUI(string mode)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnInstructorResultStateUI(mode);
        }

        public void DestroyInstructorResultStateUI(string mode)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyInstructorResultStateUI(mode);
        }

        public void SpawnStudentCountInStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentCountInStateUI();
        }

        public void DestroyStudentCountInStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentCountInStateUI();
        }

        public void SpawnStudentQuestionStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentQuestionStateUI();
        }

        public void DestroyStudentQuestionStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentQuestionStateUI();
        }

        public void SpawnStudentSubmitedStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentSubmitedStateUI();
        }

        public void DestroyStudentSubmitedStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentSubmitedStateUI();
        }

        public void AddStudentSubmitedQuestionStateEventListener(UnityAction action)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.AddStudentSubmitedQuestionStateEventListener(action);
        }

        public void RemoveStudentSubmitedQuestionStateEventListener(UnityAction action)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.RemoveStudentSubmitedQuestionStateEventListener(action);
        }

        public void SpawnStudentPreResultStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentPreResultStateUI();
        }

        public void DestroyStudentPreResultStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentPreResultStateUI();
        }

        public void SpawnStudentEndStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentEndStateUI();
        }

        public void DestroyStudentEndStateUI()
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentEndStateUI();
        }

        public void SpawnStudentResultStateUI(string mode)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.SpawnStudentResultStateUI(mode);
        }

        public void DestroyStudentResultStateUI(string mode)
        {
            if(m_QuizControllerInterface == null) return;

            m_QuizControllerInterface.DestroyStudentResultStateUI(mode);
        }

        private SlotQuiz GetSlotQuizByKeyName(string keyName)
        {
            m_SlotQuizList ??= new();

            return m_SlotQuizList.Find(slot => slot.KeyName == keyName);
        }
    }

    [Serializable]
    public class SlotQuiz
    {
        public string KeyName;

        public GameObject ControllerGameObject;
    }
}