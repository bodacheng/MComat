using UnityEngine;
using System.Collections.Generic;


namespace UnityEngine.UI
{
	[DisallowMultipleComponent, ExecuteInEditMode]
	public class UISceneManager
	{
		private static UISceneManager m_Instance;
        private List<UIScene> m_Scenes;

        private UISceneManager()
        {
            if (this.m_Scenes == null)
            {
                this.m_Scenes = new List<UIScene>();
            }
        }

		public static UISceneManager instance
		{
			get { 

                if (m_Instance == null)
                {
                    m_Instance = new UISceneManager();
                }
                return m_Instance; 
            }
		}

		

		/// <summary>
		/// Get all the registered scenes.
		/// </summary>
		public UIScene[] scenes
		{
			get { return this.m_Scenes.ToArray(); }
		}

		protected void OnDestroy()
		{
			// Remove reference
			m_Instance = null;
		}

		/// <summary>
		/// Register a scene.
		/// </summary>
		/// <param name="scene"></param>
		public void RegisterScene(UIScene scene)
		{
			// Make sure we have the list set
			if (this.m_Scenes == null)
			{
				this.m_Scenes = new List<UIScene>();
			}

			// Check if already registered
			if (this.m_Scenes.Contains(scene))
			{
				Debug.LogWarning("Trying to register a UIScene multiple times.");
				return;
			}

			// Store in the list
			this.m_Scenes.Add(scene);
		}

		/// <summary>
		/// Unregister a scene.
		/// </summary>
		/// <param name="scene"></param>
		public void UnregisterScene(UIScene scene)
		{
			if (this.m_Scenes != null)
			{
				this.m_Scenes.Remove(scene);
			}
		}

		/// <summary>
		/// Get all the active scenes.
		/// </summary>
		/// <returns></returns>
		public UIScene[] GetActiveScenes()
		{
			List<UIScene> activeScenes = this.m_Scenes.FindAll(x => x.isActivated == true);

			return activeScenes.ToArray();
		}

		/// <summary>
		/// Get the scene with the specified id. Returns null if not found.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public UIScene GetScene(int id)
		{
			if (this.m_Scenes == null || this.m_Scenes.Count == 0)
			{
				return null;
			}

			return this.m_Scenes.Find(x => x.id == id);
		}

		/// <summary>
		/// Get the next available scene id;
		/// </summary>
		/// <returns></returns>
		public int GetAvailableSceneId()
		{
			if (this.m_Scenes.Count == 0)
			{
				return 0;
			}

			int id = 0;

			foreach (UIScene scene in this.m_Scenes)
			{
				if (scene.id > id)
				{
					id = scene.id;
				}
			}

			return id + 1;
		}

		/// <summary>
		/// Transitions out of the active scene and in to the new one.
		/// </summary>
		/// <param name="scene"></param>
		public void TransitionToScene(UIScene scene)
		{
			UIScene.Transition transition = scene.transition;
			float transitionDuration = scene.transitionDuration;
			Tweens.TweenEasing transitionEasing = scene.transitionEasing;

			// Transition out of the current scenes
			UIScene[] activeScenes = this.GetActiveScenes();

			foreach (UIScene activeScene in activeScenes)
			{
				// Transition the scene out
				activeScene.TransitionOut(transition, transitionDuration, transitionEasing);
			}

			// Transition in the new scene
			scene.TransitionIn(transition, transitionDuration, transitionEasing);
		}
	}
}
