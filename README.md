# Survival
 
Move와 CameraLook 함수를 각각 FixedUpdate, LateUpdate에서 호출하는 이유에 대해 생각해보세요.
- 유니티 생명주기에 따라 처리하기 위해서 이동과 카메라가 같은 Update 안에 존재한다면 카메라 또는 이동 둘중 무엇이 먼저 먼저 작동 할지 모르기 떄문에


  별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.
  - 많은 오브젝트들이 생기고 각자가 주어진 스크립트에 따라 정해진 역할을 수행하는데 가독성, 확장성, 재사용성에서 좋다.
    
  인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.
  
  핵심 로직을 분석해보세요. (UI 스크립트 구조, `CampFire`, `DamageIndicator`)
 -IDamagable 인터페이스 구현:PlayerCondition 클래스는 IDamagalbe 인터페이스를 구현하여 물리적인 데미지를 받을 수 있는 구조를 제공합니다.
 -TakePhysicalDamage() 메서드가 구현되어 있어 외부로부터 데미지를 받을 때 체력을 감소시키고, 데미지 이벤트를 발생시킵니다.
 -CampFire에서 List<IDamagalbe> things= new List<IDamagalbe>();을 통해 IDamagalbe 인터페이스를 구현한 객체만 관리합니다.
- OnTriggerEnter / OnTriggerExit 들어오면 리스트에 추가 나가면 리스트를 제거합니다
