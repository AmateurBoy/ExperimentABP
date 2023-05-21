# Experiments ABP
![This is an image]( https://ci4.googleusercontent.com/proxy/QLv_hWGnqJHYMtct-Q84C04_qtiuvmMRBfgfGjjwH3KALMX3aGW6WkLiwWWgYtLIgZR537KlavWng_pScAkzh4v45ImNAng8gGXF4aRNzlHEdDIJJueCQWcFFJEsimRzkyhmtOwUdIfq2NKFZmWDeBhFkQBsU_Oq7gw_6NOC5M2Pq-8BzG3D51MPnxibycZFcZ9uj_OCUdSP6JsURS7yZRg2QOeU10V0Wru8poviXA=s0-d-e1-ft#https://mktdplp102neda.azureedge.net/org-8917dd3c0bbb4aecab30db7cf9e8dc5d/344e2937-26f9-eb11-94ef-000d3abd3b0c/X808Q57Fxi9qpZzF52bgj4LGwzk2CBD6YlVBVdGoL8k!)
---
��� ����� ���� �������� �� ����� �������� ������ ABP ��� �������� AB-Tests.

�� ����� ���� ������:
1. ���� ������ ������� ������ �� �������� �� �������
2. ���� ������� ������� � ������� ���� �������� �� ��� ������������ 
��������. ��� ��� �� �������� ����� � ��� ��������� ������������, 75% ������������ ������ 
���������� ����� ���� � ���� �� ���� ������ ������� �� ���������� ����

�� ����� ��� ������� ���� �������� 2 �������� � ����:
---
�������� ���� ������ � �������� �� device-token ���� �������� � ������.
```cs
public IActionResult GetExpirementButtonColor([FromQuery(Name = "device-token")] string deviceToken)
Query: https://localhost:7245/experiment/button_color?device-token=123
Return: {
  "key": "button_color",
  "value": "#FF0000"
}
```

---
�������� ���� � �������� �� device-token ���� �������� � ������.
```cs
public IActionResult GetExpirementPrice([FromQuery(Name = "device-token")] string deviceToken)
Query: https://localhost:7245/experiment/price?device-token=123
Return: {
  "key": "price",
  "value": "10"
}
```
---
����� � ���������� ������� ��� ����������� ����������.
```cs
public IActionResult GetAllStatistic()
Query: https://localhost:7245/statistic/all
```
����������� ��������� ������ ���� ����������� ������� � ������� � �� ���������.
![image](https://github.com/AmateurBoy/ExperimentABP/assets/90874301/caead06d-8a8f-4e1e-8fe6-8dc593df947e)

�� ���� ����������� � �� MSSQL.

�����-��
---
![image](https://github.com/AmateurBoy/ExperimentABP/assets/90874301/5638a0f8-6497-4193-817e-849b7ce8f71d)
---
(One-to-Many): � ������� Option ������� ������� ���� ExperimentId, ������� ��������� �� id � ������� Experiment ��� ��� � ������ ������������ ����� ���� ����� �����.

(Many-to-Many): ������� DeviceOption ������ ��������� �������� ��� ������ Device � Option. ��� �������� ������� ����� DeviceId � OptionId, ����������� �� ��������������� ������� id � �������� Device � Option (������ ������� ����� ���� ������� �� ������� �������). 


