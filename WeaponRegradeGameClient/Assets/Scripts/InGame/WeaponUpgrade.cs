using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Image weaponImage; // 검 이미지를 표시할 UI Image 컴포넌트
    public Text levelText; // 검 레벨을 표시할 UI Text 컴포넌트
    public Sprite[] weaponSprites; // 검의 다양한 레벨에 대한 이미지 배열
    private int weaponLevel = 0; // 검의 레벨 (배열의 인덱스로 사용)

    // 강화 버튼을 눌렀을 때 호출되는 메서드
    public void UpgradeWeapon()
    {
        // 현재 검 레벨을 증가시키고 배열의 범위를 벗어나지 않도록 조절
        weaponLevel = (weaponLevel + 1) % weaponSprites.Length;

        // 새로운 검 이미지로 업데이트
        weaponImage.sprite = weaponSprites[weaponLevel];

        // 현재 검 레벨을 UI Text에 업데이트
        levelText.text = "현재 검 레벨: " + (weaponLevel + 1); // 레벨은 1부터 시작하므로 +1을 해줍니다.
    }
}
