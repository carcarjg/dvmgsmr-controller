using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using RC2ClientLibrary;
using WebSocketSharp;

namespace RC2ClientDiagnostic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("RC2 Client Connection Diagnostic Tool");
            Console.WriteLine("=======================================\n");
            
            // Get connection details
            Console.Write("Enter server IP address (default: 127.0.0.1): ");
            string serverIp = Console.ReadLine();
            if (string.IsNullOrEmpty(serverIp))
                serverIp = "127.0.0.1";
                
            Console.Write("Enter server port (default: 8080): ");
            string portInput = Console.ReadLine();
            int port = string.IsNullOrEmpty(portInput) ? 8080 : int.Parse(portInput);
            
            Console.WriteLine("\n--- STEP 1: Network Connectivity Check ---");
            if (!await TestNetworkConnectivity(serverIp, port))
            {
                Console.WriteLine("\n[FAILED] Cannot reach server. Check:");
                Console.WriteLine("  1. Server IP address is correct");
                Console.WriteLine("  2. Server port is correct");
                Console.WriteLine("  3. RC2DVM application is running");
                Console.WriteLine("  4. Virtual channel is configured and started");
                Console.WriteLine("  5. Firewall is not blocking the connection");
                return;
            }
            Console.WriteLine("[OK] Server is reachable\n");
            
            Console.WriteLine("--- STEP 2: WebSocket Connection Test ---");
            if (!await TestWebSocketConnection(serverIp, port))
            {
                Console.WriteLine("\n[FAILED] WebSocket connection failed. Check:");
                Console.WriteLine("  1. RC2 server is accepting WebSocket connections on /");
                Console.WriteLine("  2. Server configuration allows connections");
                return;
            }
            Console.WriteLine("[OK] WebSocket connection successful\n");
            
            Console.WriteLine("--- STEP 3: WebRTC Signaling Test ---");
            if (!await TestWebRTCSignaling(serverIp, port))
            {
                Console.WriteLine("\n[FAILED] WebRTC signaling failed. Check:");
                Console.WriteLine("  1. Server is accepting WebRTC connections on /rtc");
                Console.WriteLine("  2. SIPSorcery dependencies are correctly installed");
                return;
            }
            Console.WriteLine("[OK] WebRTC signaling path accessible\n");
            
            Console.WriteLine("--- STEP 4: Full RC2Client Connection Test ---");
            await TestFullConnection(serverIp, port);
        }
        
        static async Task<bool> TestNetworkConnectivity(string host, int port)
        {
            try
            {
                Console.WriteLine($"Testing TCP connection to {host}:{port}...");
                using (var tcpClient = new TcpClient())
                {
                    var connectTask = tcpClient.ConnectAsync(host, port);
                    if (await Task.WhenAny(connectTask, Task.Delay(5000)) == connectTask)
                    {
                        if (tcpClient.Connected)
                        {
                            Console.WriteLine("  ✓ TCP connection successful");
                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("  ✗ Connection timeout after 5 seconds");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Connection failed: {ex.Message}");
            }
            return false;
        }
        
        static Task<bool> TestWebSocketConnection(string host, int port)
        {
            var tcs = new TaskCompletionSource<bool>();
            
            try
            {
                Console.WriteLine($"Testing WebSocket connection to ws://{host}:{port}/...");
                var ws = new WebSocket($"ws://{host}:{port}/");
                
                var timeout = Task.Delay(5000);
                
                ws.OnOpen += (sender, e) =>
                {
                    Console.WriteLine("  ✓ WebSocket opened successfully");
                    ws.Close();
                    tcs.TrySetResult(true);
                };
                
                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine($"  ✗ WebSocket error: {e.Message}");
                    tcs.TrySetResult(false);
                };
                
                ws.OnClose += (sender, e) =>
                {
                    if (!tcs.Task.IsCompleted)
                    {
                        Console.WriteLine($"  ✗ WebSocket closed: {e.Reason}");
                        tcs.TrySetResult(false);
                    }
                };
                
                ws.Connect();
                
                Task.Run(async () =>
                {
                    await timeout;
                    if (!tcs.Task.IsCompleted)
                    {
                        Console.WriteLine("  ✗ WebSocket connection timeout");
                        ws.Close();
                        tcs.TrySetResult(false);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Exception: {ex.Message}");
                tcs.TrySetResult(false);
            }
            
            return tcs.Task;
        }
        
        static Task<bool> TestWebRTCSignaling(string host, int port)
        {
            var tcs = new TaskCompletionSource<bool>();
            
            try
            {
                Console.WriteLine($"Testing WebRTC signaling path ws://{host}:{port}/rtc...");
                var ws = new WebSocket($"ws://{host}:{port}/rtc");
                
                var timeout = Task.Delay(5000);
                
                ws.OnOpen += (sender, e) =>
                {
                    Console.WriteLine("  ✓ WebRTC signaling path accessible");
                    ws.Close();
                    tcs.TrySetResult(true);
                };
                
                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine($"  ✗ WebRTC signaling error: {e.Message}");
                    tcs.TrySetResult(false);
                };
                
                ws.OnClose += (sender, e) =>
                {
                    if (!tcs.Task.IsCompleted)
                    {
                        Console.WriteLine($"  ✗ WebRTC path unavailable: {e.Reason}");
                        tcs.TrySetResult(false);
                    }
                };
                
                ws.Connect();
                
                Task.Run(async () =>
                {
                    await timeout;
                    if (!tcs.Task.IsCompleted)
                    {
                        Console.WriteLine("  ✗ WebRTC signaling timeout");
                        ws.Close();
                        tcs.TrySetResult(false);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Exception: {ex.Message}");
                tcs.TrySetResult(false);
            }
            
            return tcs.Task;
        }
        
        static async Task TestFullConnection(string host, int port)
        {
            Console.WriteLine("Testing full RC2Client connection...");
            
            var client = new RC2Client(host, port);
            bool connected = false;
            bool statusReceived = false;
            bool wsConnected = false;
            bool rtcConnected = false;
            
            // Subscribe to all events for diagnostics
            client.WebSocketConnected += (s, e) =>
            {
                Console.WriteLine("  ✓ WebSocket connected");
                wsConnected = true;
            };
            
            client.WebRtcConnected += (s, e) =>
            {
                Console.WriteLine("  ✓ WebRTC connected");
                rtcConnected = true;
            };
            
            client.StatusReceived += (s, status) =>
            {
                Console.WriteLine($"  ✓ Status received: {status.Name}");
                Console.WriteLine($"    Channel: {status.ChannelName}");
                Console.WriteLine($"    Zone: {status.ZoneName}");
                Console.WriteLine($"    State: {status.State}");
                statusReceived = true;
            };
            
            client.ErrorOccurred += (s, error) =>
            {
                Console.WriteLine($"  ✗ Error: {error}");
            };
            
            client.WebSocketDisconnected += (s, reason) =>
            {
                Console.WriteLine($"  ! WebSocket disconnected: {reason}");
            };
            
            client.WebRtcDisconnected += (s, e) =>
            {
                Console.WriteLine($"  ! WebRTC disconnected");
            };
            
            try
            {
                // Attempt connection
                connected = await client.ConnectAsync();
                
                if (connected)
                {
                    Console.WriteLine("\n[OK] Initial connection successful");
                    
                    // Wait for WebRTC to connect
                    await Task.Delay(2000);
                    
                    // Try to query status
                    Console.WriteLine("\nQuerying radio status...");
                    client.QueryStatus();
                    
                    // Wait for response
                    await Task.Delay(2000);
                    
                    // Check results
                    Console.WriteLine("\n--- Connection Summary ---");
                    Console.WriteLine($"WebSocket Connected: {(wsConnected ? "✓ YES" : "✗ NO")}");
                    Console.WriteLine($"WebRTC Connected: {(rtcConnected ? "✓ YES" : "✗ NO")}");
                    Console.WriteLine($"Status Received: {(statusReceived ? "✓ YES" : "✗ NO")}");
                    
                    if (wsConnected && rtcConnected && statusReceived)
                    {
                        Console.WriteLine("\n[SUCCESS] All systems operational!");
                        Console.WriteLine("\nYou can now use RC2Client in your application.");
                        Console.WriteLine("Try the example programs provided for usage demonstrations.");
                    }
                    else if (wsConnected && !rtcConnected)
                    {
                        Console.WriteLine("\n[PARTIAL] WebSocket works but WebRTC failed");
                        Console.WriteLine("Check:");
                        Console.WriteLine("  - SIPSorcery NuGet package is installed");
                        Console.WriteLine("  - Server supports WebRTC audio");
                        Console.WriteLine("  - Network allows UDP traffic (for ICE/STUN)");
                    }
                    else if (wsConnected && !statusReceived)
                    {
                        Console.WriteLine("\n[PARTIAL] Connected but no status received");
                        Console.WriteLine("Server may not be responding to queries.");
                        Console.WriteLine("Check server logs for errors.");
                    }
                    
                    // Keep connection open for a bit
                    Console.WriteLine("\nKeeping connection open for 5 seconds...");
                    await Task.Delay(5000);
                    
                    // Disconnect
                    Console.WriteLine("Disconnecting...");
                    client.Disconnect();
                }
                else
                {
                    Console.WriteLine("\n[FAILED] Connection failed");
                    Console.WriteLine("Check previous steps for specific issues.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] Exception during connection test:");
                Console.WriteLine($"  {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"  Inner: {ex.InnerException.Message}");
                }
                Console.WriteLine("\nStack Trace:");
                Console.WriteLine(ex.StackTrace);
            }
            
            Console.WriteLine("\n--- Diagnostic Complete ---");
        }
    }
}
