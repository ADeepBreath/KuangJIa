using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IUserInterface
{
    protected Game m_Game;
    protected GameObject m_RootUI;
    private bool m_bActive = true;

    public IUserInterface(Game game)
    {
        m_Game = game;
    }

    public bool IsVisible()
    {
        return m_bActive;
    }

    public virtual void Show()
    {
        m_RootUI.SetActive(true);
        m_bActive = true;
    }

    public virtual void Hide()
    {
        m_RootUI.SetActive(false);
        m_bActive = false;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
