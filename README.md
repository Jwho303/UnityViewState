# Unity View State
This Unity3D codebase provides a flexible and scalable framework for managing UI views with transition animations. The framework uses a state machine pattern to handle view states and transitions, ensuring smooth animations and transitions between different views.
## Structure
### Interfaces
1. IState:
Defines the interface for a state with methods to enter, update, exit, and interrupt the state.
2. IView<TEnum>:
Defines the interface for a view with generic view types.
Inherits from IState and includes additional properties and methods specific to view animations and transitions.
### Classes
1. View:
An abstract class representing a UI view.
Implements IState and manages view states using a state machine (StateMachine class).
Provides methods for initializing the view, setting animation clips, and handling transitions.
2. ViewController<TEnum>:
A class for managing multiple views.
Uses a dictionary to map view types (TEnum) to IView<TEnum> instances.
Facilitates transitioning between views.
3. StaticState:
A class representing a static state for a view.
Implements IState and provides actions to perform when entering and exiting the state.
4. TransitionState:
A class representing a transition state for a view.
Implements IState and manages transition animations.
5. StateMachine:
A class for managing the current state of a view.
Facilitates transitioning between states and handles interruptions.
## Demo
https://github.com/Jwho303/UnityViewState/assets/5286421/951a865c-5a81-47be-a5b8-fd3c1047f80e

Live demo on itch.io
https://jwho303.itch.io/unity-view-state
## Credits
"Silent" game GUI asset set by Prinbles
https://prinbles.itch.io/silent
