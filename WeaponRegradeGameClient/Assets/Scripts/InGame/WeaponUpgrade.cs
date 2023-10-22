using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public Image weaponImage; // �� �̹����� ǥ���� UI Image ������Ʈ
    public Text levelText; // �� ������ ǥ���� UI Text ������Ʈ
    public Text successProbabilityText; // ��ȭ ���� Ȯ���� ǥ���� UI Text ������Ʈ
    public Sprite[] weaponSprites; // ���� �پ��� ������ ���� �̹��� �迭
    private int weaponLevel = 0; // ���� ���� (�迭�� �ε����� ���)

    // �� ������ ��ȭ Ȯ�� �迭
    private float[] upgradeProbabilities;

    void Start()
    {
        int maxLevel = 14;
        upgradeProbabilities = new float[maxLevel];

        // �ִ� ���������� ��ȭ ���� Ȯ�� �迭�� �ʱ�ȭ�մϴ�.
        for (int i = 0; i < maxLevel; i++)
        {
            upgradeProbabilities[i] = 1.0f - (i * 0.05f);
        }
    }

    // ��ȭ ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void UpgradeWeapon()
    {
        // ������ ���� �����մϴ�.
        float randomValue = Random.Range(0f, 1f);

        // ������ �ִ� ������ �������� �ʾҰ�, ������ ���� Ȯ������ ������ ������ ������ŵ�ϴ�.
        if (weaponLevel < weaponSprites.Length - 1 && randomValue < upgradeProbabilities[weaponLevel])
        {
            // ������ ��� ���� �� ������ ������ŵ�ϴ�.
            weaponLevel++;
        }
        else
        {
            // ��ȭ�� ������ ��� ó�� �� ������ ���ư��ϴ�.
            weaponLevel = 0;
        }

        // ���� �� ������ UI Text�� ������Ʈ
        levelText.text = "���� �� ����: " + (weaponLevel + 1);

        // ��ȭ ���� Ȯ���� UI Text�� ������Ʈ
        successProbabilityText.text = "��ȭ ���� Ȯ��: " + (upgradeProbabilities[weaponLevel] * 100) + "%";

        // �� �̹��� ������Ʈ
        weaponImage.sprite = weaponSprites[weaponLevel];

        if (weaponLevel == 0)
        {
            Debug.Log("��ȭ ����! ó�� �� ������ ���ư��ϴ�.");
        }
        else
        {
            Debug.Log("��ȭ ����! ���� �� ����: " + (weaponLevel + 1));
        }
    }
}