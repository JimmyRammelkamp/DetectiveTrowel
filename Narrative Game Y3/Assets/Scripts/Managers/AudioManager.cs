//  --------------------------------------  References  --------------------------------------
//
//  Audio Manager - Introduction to AUDIO in Unity - Brackeys - https://www.youtube.com/watch?v=6OT43pvUyfY
//
//  --------------------------------------  References  --------------------------------------
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    AudioManager instance;
    [HideInInspector]
    public Sound[] sounds;

    private void Awake()
    {
        #region AudioManager Instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        #endregion
        foreach (Sound s in sounds) SoundSetUp(s);
    }

    public void SoundSetUp(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.outputAudioMixerGroup = s.outputAudioMixerGroup;
        if (s.Default)
        {
            s.source.volume = 1f;                       //  Sets Volume on AudioSource to the default value on the Sound Class
            s.source.pitch = 1f;                        //  Sets Pitch on AudioSource to the default value on the Sound Class
            s.source.spatialBlend = 0f;                 //  Sets Spatial Blend (2D(0) or 3D(1) Sound) on AudioSource to the default value on the Sound Class
        }
        else
        {
            s.source.volume = s.volume;                 //  Sets Volume on AudioSource to volume on the Sound Class
            s.source.pitch = s.pitch;                   //  Sets Pitch on AudioSource to pitch on the Sound Class
            s.source.spatialBlend = s.spacialBlend;     //  Sets Spatial Blend (2D(0) or 3D(1) Sound) on AudioSource to spacialBlend on the Sound Class
        }
        s.source.loop = s.loop;
    }


    #region Play/Stop Audio
    public void PlayAudio(string name = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (name == null)
        {
            Debug.LogError("name string is null, unable to search for Sound Class in 'sounds' array!");
            return;
        }
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopAudio(string name = null)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (name == null)
        {
            Debug.LogError("'name' string is null, unable to search for Sound Class in 'sounds' array!");
            return;
        }
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    #endregion
}

#region Editor
[CustomEditor(typeof(AudioManager))]
public class SoundEditor : Editor
{
    //  This boolean keeps track of whether or not the 'Sound' Object in the list should be Collapsed or Expanded
    private bool[] soundFoldouts;

    public override void OnInspectorGUI()
    {
        //  This shows any other Variables that are not hidden from the Inspector
        base.OnInspectorGUI();

        //  This Updates the Serialized Object
        serializedObject.Update();

        //  Finds the array of the 'Sound' named sounds in the AudioManager
        var soundList = serializedObject.FindProperty("sounds");

        //  Adds a Bold Lable for the list of sounds 
        EditorGUILayout.LabelField("Sounds", EditorStyles.boldLabel);

        //  This increases the indentation level of the GUI objects that come after
        EditorGUI.indentLevel++;

        //  This makes sure that the size of the soundFoldouts array is equal to the size of the sound list and if not it will adjust with the correct length
        if (soundFoldouts == null || soundFoldouts.Length != soundList.arraySize)
        {
            soundFoldouts = new bool[soundList.arraySize];
        }

        //  this loops throuugh all the Sound objects in the soundList
        for (int i = 0; i < soundList.arraySize; i++)
        {
            //  This finds all the serialized properties for each of the variables on the Sound object
            var sound = soundList.GetArrayElementAtIndex(i);
            var name = sound.FindPropertyRelative("name");
            var clip = sound.FindPropertyRelative("clip");
            var outputAudioMixerGroup = sound.FindPropertyRelative("outputAudioMixerGroup");
            var isDefault = sound.FindPropertyRelative("Default");
            var volume = sound.FindPropertyRelative("volume");
            var pitch = sound.FindPropertyRelative("pitch");
            var spacialBlend = sound.FindPropertyRelative("spacialBlend");
            var loop = sound.FindPropertyRelative("loop");

            //  Begin a vertical box for each sound object
            EditorGUILayout.BeginVertical(GUI.skin.box);
            //  Begin a horizontal layout for each sound object
            EditorGUILayout.BeginHorizontal();

            //  Displays the name of the sound object using the name parameter within the sound object
            soundFoldouts[i] = EditorGUILayout.Foldout(soundFoldouts[i], name.stringValue);

            //  creates a button for removing and deleting the sound object from the sound list
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                soundList.DeleteArrayElementAtIndex(i);
                break;
            }

            // Ends the horizontal layout for each sound object
            EditorGUILayout.EndHorizontal();

            // Displays all variables of the Sound Class that are a part of the sounds array as a dropdown 
            if (soundFoldouts[i])
            {
                EditorGUILayout.PropertyField(name, GUIContent.none);
                EditorGUILayout.PropertyField(clip);
                EditorGUILayout.PropertyField(outputAudioMixerGroup);
                EditorGUILayout.PropertyField(isDefault);

                if (!isDefault.boolValue) // Will only display these variables if Default is false
                {
                    EditorGUILayout.PropertyField(volume);
                    EditorGUILayout.PropertyField(pitch);
                    EditorGUILayout.PropertyField(spacialBlend);
                }

                EditorGUILayout.PropertyField(loop);
            }
            //  Ends the Vertical box each sound object
            EditorGUILayout.EndVertical();
        }
        //  Decreases the indentation level of all GUI objects after it
        EditorGUI.indentLevel--;

        //  Creates button used for creating and adding a sound object to the soundList
        if (GUILayout.Button("+"))
        {
            soundList.InsertArrayElementAtIndex(soundList.arraySize);
            soundFoldouts = new bool[soundList.arraySize];
        }

        //  This Applies any changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
#endregion