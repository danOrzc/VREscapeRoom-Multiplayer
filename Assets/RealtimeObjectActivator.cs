using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Normal.Realtime;

public class RealtimeObjectActivator :  RealtimeComponent
{
    public bool Toggle = false;
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    bool m_Activated = false;

    private ObjectActivatorModel _model;

    private ObjectActivatorModel model
    {
        set
        {
            if (_model != null)
            {
                // Unregister from events
                _model.activeValueDidChange -= ActiveValueDidChange;
                _model.toggleDidChange -= ToggleDidChange;
            }

            // Store the model
            _model = value;

            if (_model != null)
            {
                // Update the mesh render to match the new model
                UpdateActiveValue();
                UpdateToggleValue();

                // Register for events so we'll know if the color changes later
                _model.activeValueDidChange += ActiveValueDidChange;
                _model.toggleDidChange += ToggleDidChange;
            }
        }
    }

    private void ToggleDidChange(ObjectActivatorModel model, bool value)
    {
        UpdateToggleValue();
    }

    private void ActiveValueDidChange(ObjectActivatorModel model, bool value)
    {
        UpdateActiveValue();
    }

    private void UpdateActiveValue()
    {
        m_Activated = _model.activeValue;
    }    

    private void UpdateToggleValue()
    {
        if (m_Activated)
            OnActivated.Invoke();
        else
            OnDeactivated.Invoke();
    }

    public void SetToggle(bool value)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        _model.toggle = value;
    }

    public void SetActiveValue(bool value)
    {
        _model.activeValue = value;
    }

    public void Activated()
    {
        if (Toggle)
        {
            SetActiveValue(!m_Activated);
            SetToggle(!_model.toggle);
        }
        else
        {
            SetActiveValue(true);
            SetToggle(!_model.toggle);
        }
    }

    public void Deactivated()
    {
        if (!Toggle)
        {
            SetActiveValue(false);
            SetToggle(!_model.toggle);
        }
    }
}
