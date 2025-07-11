# 프로젝트 소개

개인 프로젝트로 제작한 Unity 기반 2D 탄막 회피 게임입니다.

플레이어는 시간이 지날수록 강해지는 다양한 터렛의 공격을 피하며 최대한 오래 살아남아야 합니다.

WebGL로 빌드되어 현재 아래 사이트에서 지금까지 개발된 부분까지 플레이해보실 수 있습니다.

<br/>

# 세부 사항

### 기간

2024.03.31 ~ 2024.10.12

### 주요 업무

게임의 기획, 도트 그래픽 스프라이트 제작, UI/UX 디자인 및 프로그래밍을 포함한 모든 개발 과정을 직접 담당하였습니다.

### itch.io 사이트

[닷지](https://harrrypoter.itch.io/dodge)

### 플레이 영상

[닷지 플레이 영상](https://youtu.be/Nb-ofi6hx1c?si=WBb4MgnMejv43cnM)

<br/>

# 게임 구조

![image](https://github.com/user-attachments/assets/c02a4394-bb67-4385-b15d-70f7d13afa8e)

게임의 전체적인 구조 및 흐름입니다.

플레이어는 각기 다른 능력을 가진 3종류의 캐릭터 중 하나를 선택하여 플레이할 수 있습니다. 

스테이지마다 등장하는 터렛들의 공격을 피하며 살아남아 점수를 획득해야 합니다. 일정 점수를 달성하면 잠겨 있던 다음 스테이지가 해금되어 플레이할 수 있습니다. 

모든 종류의 터렛들이 등장하는 마지막 스테이지를 해금하고, 리더보드를 통해 다른 플레이어와 경쟁해보세요.

### 기획 및 설계

[닷지 기획 및 설계](https://www.notion.so/97ccbf5ba7bd4e8bbf86c3d808d80e11?pvs=21) 

[닷지 기획 및 설계 2](https://www.notion.so/2-5c5c3ecac60c44abb4ad6686f458084f?pvs=21) 

<br/>

# 주요 기능 및 코드

1. **터렛 시스템**

<br/>

![터렛스테이지](https://github.com/user-attachments/assets/02551053-9fee-4bc5-a521-bb873f332aaa)

<br/>

[닷지 개발 일지 1 - 터렛 시스템 설계](https://www.notion.so/1-9559e4cc3149425d836e6b5dc56a7dcf?pvs=21) 

[닷지 개발 일지 2 - 터렛 강화 이벤트 시스템 설계](https://www.notion.so/2-32edddacafff4031a4ac6f4eab259368?pvs=21) 

<br/>

2. **캐릭터 어빌리티**

<br/>

![BlinkAbility-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/ff5e47f2-20e4-4fa9-92e1-57ba39e362fd)

<br/>

[닷지 개발 일지 3 - 캐릭터 특수 능력 구현](https://www.notion.so/3-1173d4b5b7b14c79b0afc37a93105b15?pvs=21) 

<br/>

3. **아이템**

<br/>

![moveSpeedUpItem](https://github.com/user-attachments/assets/db19135e-d81b-4da2-81a4-5ea5881b24d0)

<br/>

[닷지 개발 일지 4 - 아이템](https://www.notion.so/4-13b41ff392d74e5396ade33136bdab43?pvs=21) 

<br/>

4. **UI**

<br/>

![UI움직이기](https://github.com/user-attachments/assets/97d0aad8-d4c6-4453-85d9-9e312c855082)

<br/>

[닷지 개발 일지 6 - UI](https://www.notion.so/6-UI-6c359be9609b48c39339ddb64b211e20)

<br/>

# 프로젝트 경험

[닷지 개발 개선점 및 아쉬웠던 점](https://www.notion.so/2840a060b42141a698bb86206962baba?pvs=21)

<br/>

- 스탯 관리 시스템 깔끔하게 설계하기

<br/>

- 터렛 강화 이벤트
    - 이벤트 버스 패턴 적용하기

<br/>

- 터렛, 발사체, 아이템
    - 추상과 상속을 활용하여 효율적으로 개발하기

<br/>

- 캐릭터 특수 능력
    - 전략 패턴 적용하기

<br/>

- 리더보드
    - Unity Gaming Services의 리더보드 기능 활용하여 리더보드 구현하기
    - 리더보드로 스크립터블 오브젝트 대체하여 데이터베이스로 활용하기

<br/>

- UI
    - UI를 종류별로 나누고 스택으로 관리하기
    - UI를 정보 관리 스크립트, UI 창 관리 스크립트, UI 게임 오브젝트 세 가지로 나누어 구현하기

<br/>

- 아트
    - 픽셀 아트 드로잉 툴인 Aseprite를 이용하여 애니메이션, 스프라이트 제작하기

<br/>
