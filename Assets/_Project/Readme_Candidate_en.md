# Unity C# Developer Technical Task

Thank you for taking the time to complete this task.

This project is a **small Unity 2D top-down prototype**. Some parts of the codebase are intentionally rough or prototype-level, while others are clean and well-structured. This reflects real-world conditions where new systems must be built alongside existing code.

Your goal is to **implement a basic game system** and integrate it into the project.

---

## Project Overview

- Engine: **Unity (2D)**
- Input System: **Old Input Manager**
- Visuals: **Basic shapes only**
- Scene: `Main.unity`
- Player code is intentionally **clean and well-structured**
- Other systems may be **monolithic, coupled, or inconsistent**

> You are **not expected to rewrite the entire project**.

---

## Your Assignment

### Wave System
Implement a wave-based enemy progression system.

**Requirements**
- Game starts at **Wave 1**
- Enemy count per wave:
    - Wave 1: **5 enemies**
    - Each next wave: **+3 enemies**
- Enemies spawn **over time**, not all at once:
    - Spawn interval: **0.5 seconds**
- A wave completes when **all enemies from that wave are defeated**
- Between waves:
    - **3-second break**
    - Next wave starts automatically
- Display current wave in the UI:
    - `Wave: X`

> You may refactor or replace existing spawning logic as needed.

---

## Constraints & Expectations

- **Player scripts are intentionally well-structured**
    - You should not need to rewrite them
- Other systems may require fixing or refactoring
- Make **targeted, intentional improvements**
- Avoid large rewrites unless clearly justified
- Code should be readable and maintainable

---

## Deliverables

Please submit your solution as a **GitHub Pull Request**.

Include:
1. Working implementation in the Unity project
2. A short explanation in the PR description or a README comment covering:
    - What you implemented
    - What you refactored and why
    - Any trade-offs or known issues
> It will be better to have commits that show your thought process. Instead of one big commit, try to have smaller commits that show your progress.
---

## Time Expectation

- Target time: **1 day**
- This is not a test of speed or polish
- Focus on correctness, clarity, and reasoning

---

## Evaluation Criteria

We will look at:
- Ability to understand and work with existing code
- Clean and correct C# usage
- Unity fundamentals (MonoBehaviours, lifecycle, prefabs)
- Separation of responsibilities
- Sensible refactoring decisions
- Robustness of wave, scoring, and restart logic

---

## Optional (Bonus)
If time allows, you may optionally add:
- Object pooling
- ScriptableObject configuration for waves
- Simple automated tests
- Small architectural improvements

These are **not required**.

---

## Notes
This project intentionally contains imperfections.  
We are more interested in **how you reason, integrate, and improve**, than in producing a perfect or over-engineered solution.

> Good luck and thank you for your time.
