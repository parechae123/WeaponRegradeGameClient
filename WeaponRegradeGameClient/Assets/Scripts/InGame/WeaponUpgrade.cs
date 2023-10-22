using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Image weaponImage; // 검 이미지를 표시할 UI Image 컴포넌트
    public Text levelText; // 검 레벨을 표시할 UI Text 컴포넌트
    public Text successProbabilityText; // 강화 성공 확률을 표시할 UI Text 컴포넌트
    public Sprite[] weaponSprites; // 검의 다양한 레벨에 대한 이미지 배열
    private int weaponLevel = 0; // 검의 레벨 (배열의 인덱스로 사용)

    // 각 레벨별 강화 확률 배열
    private float[] upgradeProbabilities;

    void Start()
    {
        int maxLevel = 14;
        upgradeProbabilities = new float[maxLevel];

        // 최대 레벨까지의 강화 성공 확률 배열을 초기화합니다.
        for (int i = 0; i < maxLevel; i++)
        {
            upgradeProbabilities[i] = 1.0f - (i * 0.05f);
        }
    }

    // 강화 버튼을 눌렀을 때 호출되는 메서드
    public void UpgradeWeapon()
    {
        // 무작위 수를 생성합니다.
        float randomValue = Random.Range(0f, 1f);

        // 레벨이 최대 레벨에 도달하지 않았고, 무작위 수가 확률보다 작으면 레벨을 증가시킵니다.
        if (weaponLevel < weaponSprites.Length - 1 && randomValue < upgradeProbabilities[weaponLevel])
        {
            // 성공한 경우 현재 검 레벨을 증가시킵니다.
            weaponLevel++;
        }
        else
        {
            // 강화가 실패한 경우 처음 검 레벨로 돌아갑니다.
            weaponLevel = 0;
        }

        // 현재 검 레벨을 UI Text에 업데이트
        levelText.text = "현재 검 레벨: " + (weaponLevel + 1);

        // 강화 성공 확률을 UI Text에 업데이트
        successProbabilityText.text = "강화 성공 확률: " + (upgradeProbabilities[weaponLevel] * 100) + "%";

        // 검 이미지 업데이트
        weaponImage.sprite = weaponSprites[weaponLevel];

        if (weaponLevel == 0)
        {
            Debug.Log("강화 실패! 처음 검 레벨로 돌아갑니다.");
        }
        else
        {
            Debug.Log("강화 성공! 현재 검 레벨: " + (weaponLevel + 1));
        }
    }
}