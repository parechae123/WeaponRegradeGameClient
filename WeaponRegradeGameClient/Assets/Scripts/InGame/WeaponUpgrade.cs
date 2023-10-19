using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Image weaponImage; // �� �̹����� ǥ���� UI Image ������Ʈ
    public Text levelText; // �� ������ ǥ���� UI Text ������Ʈ
    public Sprite[] weaponSprites; // ���� �پ��� ������ ���� �̹��� �迭
    private int weaponLevel = 0; // ���� ���� (�迭�� �ε����� ���)

    // ��ȭ ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void UpgradeWeapon()
    {
        // ���� �� ������ ������Ű�� �迭�� ������ ����� �ʵ��� ����
        weaponLevel = (weaponLevel + 1) % weaponSprites.Length;

        // ���ο� �� �̹����� ������Ʈ
        weaponImage.sprite = weaponSprites[weaponLevel];

        // ���� �� ������ UI Text�� ������Ʈ
        levelText.text = "���� �� ����: " + (weaponLevel + 1); // ������ 1���� �����ϹǷ� +1�� ���ݴϴ�.
    }
}
