# Survival
 
Q1. 숙련 1강 ~ 숙련 3강
**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

- 입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.
- `CharacterManager`와 `Player`의 역할에 대해 고민해보세요.
- 핵심 로직을 분석해보세요 (`Move`, `CameraLook`, `IsGrounded`)
`Move`와 `CameraLook` 함수를 각각 `FixedUpdate`, `LateUpdate`에서 호출하는 이유에 대해 생각해보세요.
- 유니티 생명주기에 따라 처리하기 위해서 이동과 카메라가 같은 Update 안에 존재한다면 카메라 또는 이동 둘중 무엇이 먼저 먼저 작동 할지 모르기 떄문에


Q2. 숙련 4강 ~ 숙련 6강
**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.
- 많은 오브젝트들이 생기고 각자가 주어진 스크립트에 따라 정해진 역할을 수행하는데 가독성, 확장성, 재사용성에서 좋다.
  
인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.
- IDamagable 인터페이스 구현:PlayerCondition 클래스는 IDamagalbe 인터페이스를 구현하여 물리적인 데미지를 받을 수 있는 구조를 제공합니다.
- TakePhysicalDamage() 메서드가 구현되어 있어 외부로부터 데미지를 받을 때 체력을 감소시키고, 데미지 이벤트를 발생시킵니다.
- CampFire에서 List<IDamagalbe> things= new List<IDamagalbe>();을 통해 IDamagalbe 인터페이스를 구현한 객체만 관리합니다.
- OnTriggerEnter / OnTriggerExit 들어오면 리스트에 추가 나가면 리스트를 제거합니다
  
- 핵심 로직을 분석해보세요. (UI 스크립트 구조, `CampFire`, `DamageIndicator`)

Q3. 숙련 9강 ~ 숙련 11강
**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.
- `Interaction` 기능의 구조와 핵심 로직을 분석해보세요.
- `Inventory` 기능의 구조와 핵심 로직을 분석해보세요.
slots: ItemSlot 배열로, 아이템 슬롯을 관리합니다. 아이템의 index와 inventory를 설정해 슬롯과 인벤토리를 연결합니다.
- selectedItem 및 selectedItemIndex: 현재 선택된 아이템과 그 인덱스를 관리하는 변수입니다.
- inventoryWindow 및 관련 UI 요소: 인벤토리 창과 각 UI 텍스트 및 버튼 요소를 참조하여, 선택된 아이템의 정보를 보여주고, 버튼 활성화를 관리합니다.
- PlayerController, PlayerCondition 참조: 플레이어의 상태와 인벤토리를 조작하는 데 사용됩니다.

핵심 로직
- 아이템 추가 및 스택 관리: AddItem 메서드는 스택 가능한 아이템을 먼저 찾고, 해당 슬롯이 가득 차지 않았을 때 아이템을 스택합니다. 빈 슬롯이 없을 경우 ThrowItem을 호출 해 현재 위치에 아이템을 생성하여 드롭합니다.
- 장비 관리: OnEquipButton과 UnEquip은 플레이어가 한 번에 하나의 아이템만 장착할 수 있도록 보장합니다. 새로운 아이템을 장착하면 기존 장착 아이템을 해제하는 방식입니다.
- UI 동적 업데이트: UpdateUI, SelectItem, ClearSelectedItemWindow 메서드는 아이템 사용, 장착, 또는 버림과 같은 변화가 발생할 때마다 UI를 자동으로 업데이트하여 사용자에게 현재 인벤토리 상태를 정확히 보여줍니다.

