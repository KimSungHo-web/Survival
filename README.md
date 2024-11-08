# Survival

 **분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.
 
## Q1. 숙련 1강 ~ 숙련 3강

### 입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.
- (empty)
### `CharacterManager`와 `Player`의 역할에 대해 고민해보세요.
- (empty)
### 핵심 로직을 분석해보세요 (`Move`, `CameraLook`, `IsGrounded`)
- (empty)
### `Move`와 `CameraLook` 함수를 각각 `FixedUpdate`, `LateUpdate`에서 호출하는 이유에 대해 생각해보세요.
- 유니티 생명주기에 따라 처리하기 위해서 이동과 카메라가 같은 Update 안에 존재한다면 카메라 또는 이동 둘중 무엇이 먼저 먼저 작동 할지 모르기 떄문에
----
## Q2. 숙련 4강 ~ 숙련 6강
**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.
* * *
### 별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.
- 많은 오브젝트들이 생기고 각자가 주어진 스크립트에 따라 정해진 역할을 수행하는데 가독성, 확장성, 재사용성에서 좋다.

### 인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.
- IDamagable 인터페이스 구현:PlayerCondition 클래스는 IDamagalbe 인터페이스를 구현하여 물리적인 데미지를 받을 수 있는 구조를 제공합니다.
- TakePhysicalDamage() 메서드가 구현되어 있어 외부로부터 데미지를 받을 때 체력을 감소시키고, 데미지 이벤트를 발생시킵니다.
- CampFire에서 List<IDamagalbe> things= new List<IDamagalbe>();을 통해 IDamagalbe 인터페이스를 구현한 객체만 관리합니다.
- OnTriggerEnter / OnTriggerExit 들어오면 리스트에 추가 나가면 리스트를 제거합니다

### 핵심 로직을 분석해보세요. (UI 스크립트 구조, `CampFire`, `DamageIndicator`)
- (empty)
---
## Q3. 숙련 9강 ~ 숙련 11강

### `Interaction` 기능의 구조와 핵심 로직을 분석해보세요.

<주요 변수>
- `cheackRate`와 'lastCheckTime': 상호작용할 수 있는 오브젝트를 탐지하는 빈도 설정(초 단위)
- `maxCheckDistance`: 탐지할 수 있는 최대 거리
- `layerMask`: 레이어 마스크ㅡ로 특정 레이어에 있는 오브젝트만 탐지
- `curIntractGameObject`: 현재 상호작용할 수 있는 오브젝트
- `curInteracterable`: 상호작용 인터페이스(`IteamObject`의 `IInteractable` 인터페이스)를 구현한 현재 오브젝트 참조
- `prompt text`: 상호작용 프롬프트를 표시할 `TextMeshProUGUI` 텍스트
- `camera`: 메인 카메라

<Start()메서드>
- `camera`와 `prompt text`를 초기화
- "'Prompt text'"라는 이름의 게임 오브젝트가 씬에 존재할 경우 텍스트 컴포넌트를 할당

<Update()메서드>
 - 'checkRate' 간격으로 실행, 플레이어 앞에 상호작용 가능한 오브젝트가 있으면 감지
 - 카메라의 중앙에 `Raycast`를 쏘아 감지하고,`Physics.Raycast()`를 이용해 `maxCheckDistance` 안에 있는 상호작용 가능한 오브젝트를 찾음
 - 탐지된 오브젝트가 `curIntractGameObject`와 다른 경우
  > `curIntractGameObject`와 `curIntratable`을 갱신하고 `SetPromptText()`로 프롬프트를 표시함
 - 오브젝트가 감지되지 않으면 `curIntractable`과 `curIntractGameObject`룰 null로 설정하고 promptText를 비활성화함

<핵심로직>
- 탐지 주기: checkRate에 따라 주기적으로 앞에 있는 상호작용 가능한 오브젝트 감지
- 상호작용 프롬프트 표시: 오브젝트 탐지 시, 해당 오브젝트의 프롬프트 메시지를 화면에 표시
- 상호작용: 상호작용 입력시 인터페이스를 통해 상호작용 메서드를 호출하여 로직 수행 후 초기화
----
### `Inventory` 기능의 구조와 핵심 로직을 분석해보세요.

<주요 변수>
- 'slots': 'ItemSlot' 배열로, 아이템 슬롯을 관리합니다. 아이템의 'index'와 'inventory'를 설정해 슬롯과 인벤토리를 연결합니다.
- 'selectedItem' 및 'selectedItemIndex': 현재 선택된 아이템과 그 인덱스를 관리하는 변수입니다.
- 'inventoryWindow' 및 관련 UI 요소: 인벤토리 창과 각 UI 텍스트 및 버튼 요소를 참조하여, 선택된 아이템의 정보를 보여주고, 버튼 활성화를 관리합니다.
- 'PlayerController', 'PlayerCondition' 참조: 플레이어의 상태와 인벤토리를 조작하는 데 사용됩니다.

<핵심 로직>
- 아이템 추가 및 스택 관리: 'AddItem' 메서드는 스택 가능한 아이템을 먼저 찾고, 해당 슬롯이 가득 차지 않았을 때 아이템을 스택합니다. 빈 슬롯이 없을 경우 'ThrowItem'을 호출 해 현재 위치에 아이템을 생성하여 드롭합니다.
- 장비 관리: 'OnEquipButton'과 'UnEquip'은 플레이어가 한 번에 하나의 아이템만 장착할 수 있도록 보장합니다. 새로운 아이템을 장착하면 기존 장착 아이템을 해제하는 방식입니다.
- UI 동적 업데이트: 'UpdateUI', 'SelectItem', 'ClearSelectedItemWindow' 메서드는 아이템 사용, 장착, 또는 버림과 같은 변화가 발생할 때마다 UI를 자동으로 업데이트하여 사용자에게 현재 인벤토리 상태를 정확히 보여줍니다.

### Q1. 숙련 12강 ~ 숙련 14강

### [요구사항 1]

**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

### - Equipment와 EquipTool 기능의 구조와 핵심 로직을 분석해보세요.

분석 :

핵심 로직 분석(Equipment, EquipTool )

-EquipTool
     - public override void OnAttackInput()
	- Equip에서 OnAttackInput()을 virtual로 선언하여 자식인 EquipTool에서 override함
	- 우선 공격중이 아닌지를 확인하고, 캐릭터 매니저 싱글톤에 플레이어 컨디션 UseStamina()를 확인합니다.
	- UseStamina() 값은 미리 지정한 useStamina의 값을 넣어주며 attacking을 참으로 변경, Attack이라는 애니메이션트리거를 작동, OnCanAttack을 attackRate만큼 지연시키기위해 Invoke를 사용합니다.

     - Onhit()
	-
 ----
