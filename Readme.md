# 🤖 RoboTon

A blockchain-powered Match-3 puzzle game built for the **TON Crypto Hackathon**. Combines classic puzzle gameplay with TON blockchain integration for wallet connectivity and competitive leaderboards.

[![Unity](https://img.shields.io/badge/Unity-2022.3.25f1-black.svg?style=flat&logo=unity)](https://unity.com/)
[![TON](https://img.shields.io/badge/TON-Blockchain-0088cc.svg?style=flat)](https://ton.org/)
[![WebGL](https://img.shields.io/badge/Platform-WebGL-purple.svg?style=flat)](https://docs.unity3d.com/Manual/webgl.html)

## 🎮 About the Game

RoboTon is a fast-paced Match-3 puzzle game where players swap adjacent tiles to create matches of three or more identical items. Built with Unity and integrated with TON blockchain, the game offers a seamless Web3 gaming experience with wallet connectivity, persistent player profiles, and global leaderboards.

**Play directly in your browser - no downloads required!**

## ✨ Key Features

### 🎯 Gameplay
- **Classic Match-3 Mechanics**: Intuitive tile-swapping gameplay
- **Score System**: Track your performance with real-time scoring
- **Progressive Difficulty**: Increasingly challenging levels
- **Sound Effects**: Engaging audio feedback for matches and interactions

### 🔗 Blockchain Integration
- **TonConnect Integration**: Seamless wallet connectivity
- **Decentralized Profiles**: Your progress stored securely on TON blockchain
- **Verified Leaderboards**: Compete with players worldwide
- **Web3 Authentication**: Blockchain-based player verification

### 🎨 User Experience
- **WebGL Optimized**: Play directly in modern browsers
- **Responsive UI**: Adaptive interface for different screen sizes
- **Custom Template**: Tailored WebGL template (TC_Minimal) for optimal performance
- **QR Code Support**: Easy wallet connection via mobile devices

## 🛠️ Technical Stack

| Component | Technology |
|-----------|-----------|
| Game Engine | Unity 2022.3.25f1 |
| Programming Language | C# |
| Blockchain | TON (The Open Network) |
| Wallet SDK | TonConnect |
| Deployment Platform | WebGL |
| UI Framework | Unity UI System |

## 📁 Project Architecture
```aiignore
RoboTon/
├── Assets/
│   ├── Scripts/
│   │   ├── Game/                           # Core game logic
│   │   │   ├── GridManager.cs              # Match-3 grid management
│   │   │   ├── TileItem.cs                 # Individual tile behavior
│   │   │   ├── ScoreManager.cs             # Score tracking & calculation
│   │   │   ├── ProfileManager.cs           # Player data & blockchain sync
│   │   │   └── SoundManager.cs             # Audio management
│   │   ├── UI/                             # User interface
│   │   │   ├── MainMenuWindow.cs           # Main menu screen
│   │   │   ├── GameInProgressWindow.cs     # Active gameplay UI
│   │   │   ├── ProfileWindow.cs            # Player stats display
│   │   │   ├── LeaderboardWindow.cs        # Global rankings
│   │   │   ├── LeaderboardItem.cs          # Individual leaderboard entry
│   │   │   ├── MenuRouter.cs               # Screen navigation
│   │   │   ├── PopupRouter.cs              # Popup management
│   │   │   ├── LoadingPopup.cs             # Loading states
│   │   │   └── ErrorPopup.cs               # Error handling
│   │   └── URLParameters.cs                # Web integration utilities
│   ├── TonConnect/                         # TON blockchain SDK
│   │   ├── Scripts/                        # TonConnect implementation
│   │   │   ├── TonConnect/                 # Core TonConnect logic
│   │   │   ├── QR Code/                    # QR code generation
│   │   │   └── Other/                      # Utility scripts
│   │   ├── Images/                         # Blockchain-related assets
│   │   ├── UI Toolkit/                     # Custom UI components
│   │   └── Scenes/                         # TonConnect demo scenes
│   ├── Scenes/
│   │   └── Main.unity                      # Main game scene
│   ├── Prefabs/                            # Reusable game objects
│   ├── Images/
│   │   ├── Items/                          # Tile sprites
│   │   │   ├── fullpack.png                # Tile sprite asset
│   │   │   ├── packchip.png                # Tile sprite asset
│   │   │   └── 9.png                       # Tile sprite asset
│   │   ├── Backgrounds/                    # Background graphics
│   │   ├── UiElements/                     # UI components
│   │   ├── Logo.png                        # Game logo
│   │   └── rectangle.png                   # UI shape
│   ├── Sounds/                             # Audio files
│   ├── Fonts/                              # Typography assets
│   ├── Resources/                          # Runtime-loaded assets
│   ├── Plugins/                            # Native plugins & libraries
│   ├── Editor/                             # Unity Editor extensions
│   ├── WebGLTemplates/
│   │   └── TC_Minimal/                     # Custom WebGL template
│   ├── packages.config                     # NuGet packages
│   └── NuGet.config                        # NuGet configuration
├── Packages/
│   ├── manifest.json                       # Unity package manifest
│   └── packages-lock.json                  # Package version lock
├── ProjectSettings/                        # Unity project configuration
├── .github/
│   └── workflows/                          # CI/CD workflows
├── out/                                    # Build output folder
├── output/                                 # Alternative build folder
├── newout/                                 # Alternative build folder
├── .gitignore                              # Git ignore rules
├── .vsconfig                               # Visual Studio configuration
└── README.md                               # Project documentation
```
## 🎯 Core Game Components

### Game Mechanics Layer
- **GridManager**: Orchestrates the match-3 grid, handles tile spawning, match detection, and chain reactions
- **TileItem**: Manages individual tile states, animations, and interactions
- **ScoreManager**: Calculates scores, tracks combos, and manages game progression
- **SoundManager**: Controls audio playback for game events

### Blockchain Layer
- **ProfileManager**: Synchronizes player data with TON blockchain
- **TonConnect Integration**: Handles wallet connectivity and authentication
- **Leaderboard System**: Verifies and displays player rankings on-chain

### UI Layer
- **Window System**: Modular screen management (Menu, Game, Profile, Leaderboard)
- **Popup System**: Dynamic popups for loading states and error handling
- **Router Components**: Navigation flow between different game states

## 🚀 Getting Started

### For Players
Simply visit the game URL and connect your TON wallet to start playing!

### For Developers

#### Prerequisites
- **Unity Hub** with Unity 2022.3.25f1 installed
- **Git** for version control
- **TON Wallet** (e.g., Tonkeeper, MyTonWallet) for testing blockchain features
- **Web Server** (optional) for local WebGL testing

#### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/RoboTon.git
   cd RoboTon
   ```

2. **Open in Unity**
    - Launch Unity Hub
    - Click "Open" and select the RoboTon folder
    - Wait for Unity to import all assets

3. **Configure TonConnect** (if needed)
    - Check Assets/TonConnect for configuration files
    - Ensure manifest URL is correctly set for your deployment

4. **Test in Editor**
    - Open `Assets/Scenes/Main.unity`
    - Press the Play button to test gameplay
    - Note: Some blockchain features may require WebGL build

#### Building for WebGL

1. **Configure Build Settings**
   ```
   File → Build Settings
   Platform → WebGL
   Click "Switch Platform"
   ```

2. **Player Settings** (Optional optimizations)
   ```
   Edit → Project Settings → Player
   Resolution and Presentation:
   - WebGL Template: TC_Minimal
   - Compression Format: Gzip (recommended)
   ```

3. **Build the Project**
    - Click "Build" or "Build And Run"
    - Select output folder (e.g., `output/` or `build/`)
    - Wait for compilation to complete

4. **Deploy**
    - Upload build folder to web hosting service
    - Ensure proper MIME types for .wasm and .data files
    - Test wallet connectivity in production environment

## 🔗 TON Blockchain Integration

### Features Powered by TON

| Feature | Implementation | Benefit |
|---------|---------------|---------|
| **Authentication** | TonConnect SDK | Secure, passwordless login |
| **Player Profiles** | On-chain storage | Persistent data across devices |
| **Leaderboards** | Smart contract integration | Tamper-proof rankings |
| **Wallet Connection** | QR code + deep links | Mobile & desktop support |

### How It Works

1. **Player connects** TON wallet via TonConnect
2. **Game authenticates** player's wallet address
3. **Profile loads** from blockchain (or creates new)
4. **Scores sync** to blockchain after each game
5. **Leaderboard updates** in real-time with verified results

## 🎨 Game Assets

The game features custom-designed assets optimized for web performance:
- **Robot-themed tiles** (fullpack.png, packchip.png)
- **Modern UI elements** for seamless navigation
- **Particle effects** for match animations
- **Background graphics** with sci-fi aesthetic
- **Sound effects** for immersive gameplay

## 🏆 Built for TON Hackathon

### Hackathon Highlights
✅ **Blockchain Integration**: Native TON wallet support via TonConnect  
✅ **Web3 Gaming**: Seamless crypto-gaming experience  
✅ **Decentralized Leaderboards**: Fair, transparent competition  
✅ **Cross-platform**: Play anywhere with WebGL deployment  
✅ **User-friendly**: No blockchain knowledge required to play

### Innovation Points
- **Mainstream appeal**: Classic game genre + crypto features
- **Low barrier**: Browser-based, no installation needed
- **TON ecosystem**: Showcases TonConnect capabilities
- **Scalable design**: Foundation for future Web3 features (NFTs, tokens, etc.)

## 🐛 Troubleshooting

### Common Issues

**Wallet won't connect**
- Ensure you're using a compatible TON wallet (Tonkeeper, MyTonWallet)
- Check if browser blocks popups
- Try QR code connection method

**Game doesn't load**
- Clear browser cache
- Use modern browser (Chrome, Firefox, Edge)
- Check internet connection

**Score not saving**
- Verify wallet is still connected
- Check blockchain network status
- Reload page and reconnect wallet

## 📄 License

This project was created for the TON Crypto Hackathon. Please refer to the project license for usage terms.

## 🤝 Contributing

We welcome contributions! Whether you're fixing bugs, improving documentation, or proposing new features:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👥 Team

Created with ❤️ for the TON Crypto Hackathon

## 🔗 Links

- [TON Blockchain](https://ton.org/)
- [TonConnect Documentation](https://github.com/ton-connect)
- [Unity Documentation](https://docs.unity3d.com/)

---

**🎮 Play now and climb the leaderboard!**  
*Match tiles, earn points, compete globally on TON blockchain*