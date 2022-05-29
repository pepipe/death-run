using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace pepipe.DeathRun.Player
{
    public class RandomPlayer : MonoBehaviour {
        [SerializeField] List<GameObject> m_Characters;

        int _currentChar;

        void Awake() {
            _currentChar = Random.Range(0, m_Characters.Count);
            m_Characters[_currentChar].SetActive(true);
            DisableAllOtherCharacters();
        }

        public void NextChar() {
            _currentChar = _currentChar + 1 >= m_Characters.Count
                ? 0
                : _currentChar + 1;
            
            DisableAllOtherCharacters();
            m_Characters[_currentChar].SetActive(true);
        }
        
        void DisableAllOtherCharacters() {
            foreach (var character in m_Characters.Where(c => c != m_Characters[_currentChar]))
                character.SetActive(false);
        }
    }
}
