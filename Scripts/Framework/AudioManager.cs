using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	[System.Serializable]
	public class DwarfAudioClips {
		public List<AudioClip> gameStartClips, attackClips, getDamageOnFullHealthClips, getDamageOnMediumHealthClips, getDamageOnSmallHealthClips, dangerZoneClips, pullPositionClips, winClips, loseClips;
	}

	[System.Serializable]
	public class GoblinAudioClips {
		public List<AudioClip> gameStartClips, attackClips, getDamageOnFullHealthClips, getDamageOnMediumHealthClips, getDamageOnSmallHealthClips, dangerZoneClips, pullPositionClips, winClips, loseClips;
	}

	[SerializeField]
	private DwarfAudioClips dwarfAudioClips;

	[SerializeField]
	private GoblinAudioClips goblinAudioClips;

	private AudioSource audioSource;
	private AudioClip audioClip;

	private void Awake () {
		audioSource = GetComponent<AudioSource>();
	}

	private void Start () {
		StartCoroutine (StartGameWithClips());
	}

	private IEnumerator StartGameWithClips () {
		audioSource.clip = dwarfAudioClips.gameStartClips[0];
		audioSource.Play ();
		yield return new WaitForSeconds ((dwarfAudioClips.gameStartClips[0].length)+0.3f);
		audioSource.clip = goblinAudioClips.gameStartClips[0];
		audioSource.Play();
	}

	public void PlayAttackClip (PlayerType playerType) {
		switch (playerType) {
		case PlayerType.DWARF:
			audioClip = dwarfAudioClips.attackClips[GameManager.random.Next (0, dwarfAudioClips.attackClips.Count-1)];
			break;
		case PlayerType.GOBLIN:
			audioClip = goblinAudioClips.attackClips[GameManager.random.Next (0, goblinAudioClips.attackClips.Count-1)];
			break;

		default:
			break;
		}
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayGetDamageClips (PlayerType playerType, int healthAmount) {
		switch (playerType) {
		case PlayerType.DWARF:
			if (healthAmount < 20 && healthAmount >= 14) {
				audioClip = dwarfAudioClips.getDamageOnFullHealthClips[GameManager.random.Next (0, dwarfAudioClips.getDamageOnFullHealthClips.Count-1)];
			} else if (healthAmount < 14 && healthAmount >= 6) {
				audioClip = dwarfAudioClips.getDamageOnMediumHealthClips[GameManager.random.Next (0, dwarfAudioClips.getDamageOnMediumHealthClips.Count-1)];
			} else if (healthAmount < 6 && healthAmount > 0) {
				audioClip = dwarfAudioClips.getDamageOnSmallHealthClips[GameManager.random.Next (0, dwarfAudioClips.getDamageOnSmallHealthClips.Count-1)];
			}
			break;
		case PlayerType.GOBLIN:
			if (healthAmount < 20 && healthAmount >= 14) {
				audioClip = goblinAudioClips.getDamageOnFullHealthClips[GameManager.random.Next (0, goblinAudioClips.getDamageOnFullHealthClips.Count-1)];
			} else if (healthAmount < 14 && healthAmount >= 6) {
				audioClip = goblinAudioClips.getDamageOnMediumHealthClips[GameManager.random.Next (0, goblinAudioClips.getDamageOnMediumHealthClips.Count-1)];
			} else if (healthAmount < 6 && healthAmount > 0) {
				audioClip = goblinAudioClips.getDamageOnSmallHealthClips[GameManager.random.Next (0, goblinAudioClips.getDamageOnSmallHealthClips.Count-1)];
			}
			break;

		default:
			break;
		}

		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayDangerZoneClip (PlayerType playerType) {
		if (Random.value < 0.5f)
			return;

		switch (playerType) {
		case PlayerType.DWARF:
			audioClip = dwarfAudioClips.dangerZoneClips[GameManager.random.Next (0, dwarfAudioClips.dangerZoneClips.Count-1)];
			break;
		case PlayerType.GOBLIN:
			audioClip = goblinAudioClips.dangerZoneClips[GameManager.random.Next (0, goblinAudioClips.dangerZoneClips.Count-1)];
			break;

		default:
			break;
		}
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayPullPositionClip (PlayerType playerType) {
		switch (playerType) {
		case PlayerType.DWARF:
			audioClip = dwarfAudioClips.pullPositionClips[GameManager.random.Next (0, dwarfAudioClips.pullPositionClips.Count-1)];
			break;
		case PlayerType.GOBLIN:
			audioClip = goblinAudioClips.pullPositionClips[GameManager.random.Next (0, goblinAudioClips.pullPositionClips.Count-1)];
			break;

		default:
			break;
		}
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayWinClip (PlayerType playerType) {
		switch (playerType) {
		case PlayerType.DWARF:
			audioClip = dwarfAudioClips.winClips[GameManager.random.Next (0, dwarfAudioClips.winClips.Count-1)];
			break;
		case PlayerType.GOBLIN:
			audioClip = goblinAudioClips.winClips[GameManager.random.Next (0, goblinAudioClips.winClips.Count-1)];
			break;

		default:
			break;
		}
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayLoseClip (PlayerType playerType) {
		switch (playerType) {
		case PlayerType.DWARF:
			audioClip = dwarfAudioClips.loseClips[GameManager.random.Next (0, dwarfAudioClips.loseClips.Count-1)];
			break;
		case PlayerType.GOBLIN:
			audioClip = goblinAudioClips.loseClips[GameManager.random.Next (0, goblinAudioClips.loseClips.Count-1)];
			break;

		default:
			break;
		}
		audioSource.clip = audioClip;
		audioSource.Play ();
	}
}
