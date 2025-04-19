# HFSM
Unityで使用できる簡易階層型ステートマシン

# 基本的な使い方
## 1. ステートマシンを作成
```cs
private StateMachine<string> stateMachine;
stateMachine = new StateMachine<string>();
```

## 2. 状態を追加
BaseActionクラスを継承したクラスを作成
```cs
var aState = new AState();
var bState = new BState();
```
ステートマシン作成時に設定した型で状態IDを設定
```cs
stateMachine.AddState("A-State", aState);
stateMachine.AddState("B-State", bState);
```

## 3. 遷移を追加
```cs
var aToB = stateMachine.AddTransition("A-State", "B-State");
var bToA = stateMachine.AddTransition("B-State", "A-State");
```

## 4. 遷移条件を設定
### ラムダ式を用いた設定方法
```cs
var isInputKeySpace = new Condition(() => Input.GetKeyDown(KeyCode.Space));
```
### 派生クラスを用いた設定方法
1. IConditionを継承したクラスを作成
```cs
public class ConditionIsInputKeySpace : ICondition
{
    public bool Decide()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
```
2. クラスをインスタンスする
```cs
var isInputKeySpace = new ConditionIsInputKeySpace();
```
### パラメータを用いた設定方法
1. パラメータを作成
```cs
var parameter = new Parameter<string>();
```
2. パラメータを追加
```cs
parameter.Add<bool>("IsInputKeySpace", false);
```
3. パラメータを設定
```cs
var isInputKeySpace = new Condition(() => parameter.Get<bool>("IsInputKeySpace"));
```
4. 他クラス(アクション等)でパラメータの値をいじる
```cs
parameter.Set<bool>("IsInputKeySpace", Input.GetKeyDown(KeyCode.Space));
```

## 5. 遷移条件を追加
```cs
aToB.AddCondition(isInputKeySpace, true);
bToA.AddCondition(isInputKeySpace, false);
```

## 6. 初期状態の設定
```cs
stateMachine.SetInitState("A-State");
```

## 8. アクション内で使用するイベント関数を設定
```cs
private void Start()
{
    stateMachine.Initialize();
}

private void FixedUpdate()
{
    stateMachine.OnFixedUpdate();
}

private void Update()
{
    stateMachine.OnUpdate();
}

private void OnDrawGizmos()
{
    // ?(null条件演算子)の追加必須
    stateMachine?.OnDrawDebug();
}
```

### その他
<T>内の型はジェネリックのためenumやint、string等に対応
```cs
public enum State
{
    AState,
    BState
}

private StateMachine<State> stateMachine;
```

どの状態からでも遷移可能にする
```cs
stateMachine.AddAnyTransition(State.BState);
```
